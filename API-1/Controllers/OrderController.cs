/*
 * Auteurs: Mohamed Ali Bachar et Wasim Bessaou
 * Fichier: OrderController.cs
 * Projet: API_1
 * Date de création: 2024-05-31
 * Description: Ce fichier contient le contrôleur OrderController qui gère les opérations CRUD pour les commandes.
 */

using API_1.Data;
using API_1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly DataContext _context;

        public OrderController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupérer toutes les commandes.
        /// </summary>
        /// <returns>Une liste de commandes</returns>
        /// <response code="200">Retourne la liste des commandes</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Order>), 200)]
        public async Task<ActionResult<List<Order>>> GetOrders()
        {
            var orders = await _context.Orders.ToListAsync();
            return Ok(orders);
        }

        /// <summary>
        /// Récupérer une commande spécifique par son ID.
        /// </summary>
        /// <param name="id">ID de la commande</param>
        /// <returns>La commande demandée</returns>
        /// <response code="200">Retourne la commande demandée</response>
        /// <response code="404">Si la commande n'est pas trouvée</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Order), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound("Aucune commande trouvée avec l'ID: " + id);
            }
            return Ok(order);
        }

        /// <summary>
        /// Créer une nouvelle commande.
        /// </summary>
        /// <param name="order">La commande à créer</param>
        /// <returns>La commande créée</returns>
        /// <response code="201">Retourne la nouvelle commande créée</response>
        [HttpPost]
        [ProducesResponseType(typeof(Order), 201)]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrderById), new { id = order.OrderId }, order);
        }

        /// <summary>
        /// Mettre à jour les informations d'une commande.
        /// </summary>
        /// <param name="id">ID de la commande</param>
        /// <param name="order">La commande mise à jour</param>
        /// <returns>La commande mise à jour</returns>
        /// <response code="200">Retourne la commande mise à jour</response>
        /// <response code="400">Si l'ID est invalide</response>
        /// <response code="404">Si la commande n'est pas trouvée</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Order), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Order>> UpdateOrder(int id, [FromBody] Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest("ID de commande invalide.");
            }

            var existingOrder = await _context.Orders.FindAsync(id);
            if (existingOrder == null)
            {
                return NotFound("Aucune commande trouvée pour l'ID: " + id);
            }

            existingOrder.UserId = order.UserId;
            existingOrder.OrderDate = order.OrderDate;

            await _context.SaveChangesAsync();

            return Ok(existingOrder);
        }

        /// <summary>
        /// Supprimer une commande.
        /// </summary>
        /// <param name="id">ID de la commande</param>
        /// <response code="204">Si la commande est supprimée avec succès</response>
        /// <response code="404">Si la commande n'est pas trouvée</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound("Aucune commande trouvée avec l'ID: " + id);
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
