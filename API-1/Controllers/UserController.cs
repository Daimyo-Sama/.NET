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
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupérer tous les utilisateurs.
        /// </summary>
        /// <returns>Une liste d'utilisateurs</returns>
        /// <response code="200">Retourne la liste des utilisateurs</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<User>), 200)]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        /// <summary>
        /// Récupérer un utilisateur spécifique par son ID.
        /// </summary>
        /// <param name="id">ID de l'utilisateur</param>
        /// <returns>L'utilisateur demandé</returns>
        /// <response code="200">Retourne l'utilisateur demandé</response>
        /// <response code="404">Si l'utilisateur n'est pas trouvé</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("Aucun utilisateur trouvé avec l'ID: " + id);
            }
            return Ok(user);
        }

        /// <summary>
        /// Créer un nouvel utilisateur.
        /// </summary>
        /// <param name="user">L'utilisateur à créer</param>
        /// <returns>L'utilisateur créé</returns>
        /// <response code="201">Retourne le nouvel utilisateur créé</response>
        [HttpPost]
        [ProducesResponseType(typeof(User), 201)]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
        }

        /// <summary>
        /// Mettre à jour les informations d'un utilisateur.
        /// </summary>
        /// <param name="id">ID de l'utilisateur</param>
        /// <param name="user">L'utilisateur mis à jour</param>
        /// <returns>L'utilisateur mis à jour</returns>
        /// <response code="200">Retourne l'utilisateur mis à jour</response>
        /// <response code="400">Si l'ID est invalide</response>
        /// <response code="404">Si l'utilisateur n'est pas trouvé</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<User>> UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.UserId)
            {
                return BadRequest("ID utilisateur invalide.");
            }

            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound("Aucun utilisateur trouvé pour l'ID: " + id);
            }

            existingUser.Username = user.Username;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;

            await _context.SaveChangesAsync();

            return Ok(existingUser);
        }

        /// <summary>
        /// Supprimer un utilisateur.
        /// </summary>
        /// <param name="id">ID de l'utilisateur</param>
        /// <response code="204">Si l'utilisateur est supprimé avec succès</response>
        /// <response code="404">Si l'utilisateur n'est pas trouvé</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("Aucun utilisateur trouvé avec l'ID: " + id);
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
