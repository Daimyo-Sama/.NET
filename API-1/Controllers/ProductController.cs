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
    public class ProductController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupérer tous les produits.
        /// </summary>
        /// <returns>Une liste de produits</returns>
        /// <response code="200">Retourne la liste des produits</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), 200)]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        /// <summary>
        /// Récupérer un produit spécifique par son ID.
        /// </summary>
        /// <param name="id">ID du produit</param>
        /// <returns>Le produit demandé</returns>
        /// <response code="200">Retourne le produit demandé</response>
        /// <response code="404">Si le produit n'est pas trouvé</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("Aucun produit trouvé avec l'ID: " + id);
            }
            return Ok(product);
        }

        /// <summary>
        /// Créer un nouveau produit.
        /// </summary>
        /// <param name="product">Le produit à créer</param>
        /// <returns>Le produit créé</returns>
        /// <response code="201">Retourne le nouveau produit créé</response>
        [HttpPost]
        [ProducesResponseType(typeof(Product), 201)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);
        }

        /// <summary>
        /// Mettre à jour les informations d'un produit.
        /// </summary>
        /// <param name="id">ID du produit</param>
        /// <param name="product">Le produit mis à jour</param>
        /// <returns>Le produit mis à jour</returns>
        /// <response code="200">Retourne le produit mis à jour</response>
        /// <response code="400">Si l'ID est invalide</response>
        /// <response code="404">Si le produit n'est pas trouvé</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Product>> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest("ID de produit invalide.");
            }

            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return NotFound("Aucun produit trouvé pour l'ID: " + id);
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;

            await _context.SaveChangesAsync();

            return Ok(existingProduct);
        }

        /// <summary>
        /// Supprimer un produit.
        /// </summary>
        /// <param name="id">ID du produit</param>
        /// <response code="204">Si le produit est supprimé avec succès</response>
        /// <response code="404">Si le produit n'est pas trouvé</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("Aucun produit trouvé avec l'ID: " + id);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
