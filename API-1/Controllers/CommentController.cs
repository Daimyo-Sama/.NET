/*
 * Auteurs: Mohamed Ali Bachar et Wasim Bessaou
 * Fichier: CommentController.cs
 * Projet: API_1
 * Date de création: 2024-05-31
 * Description: Ce contrôleur fournit des points de terminaison pour gérer les commentaires,
 *              y compris la création, la lecture, la mise à jour et la suppression des commentaires.
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
    public class CommentController : ControllerBase
    {
        private readonly DataContext _context;

        public CommentController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupérer tous les commentaires.
        /// </summary>
        /// <returns>Une liste de commentaires</returns>
        /// <response code="200">Retourne la liste des commentaires</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Comment>), 200)]
        public async Task<ActionResult<List<Comment>>> GetComments()
        {
            var comments = await _context.Comments.ToListAsync();
            return Ok(comments);
        }

        /// <summary>
        /// Récupérer un commentaire spécifique par son ID.
        /// </summary>
        /// <param name="id">ID du commentaire</param>
        /// <returns>Le commentaire demandé</returns>
        /// <response code="200">Retourne le commentaire demandé</response>
        /// <response code="404">Si le commentaire n'est pas trouvé</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Comment), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Comment>> GetCommentById(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound("Aucun commentaire trouvé avec l'ID: " + id);
            }
            return Ok(comment);
        }

        /// <summary>
        /// Créer un nouveau commentaire.
        /// </summary>
        /// <param name="comment">Le commentaire à créer</param>
        /// <returns>Le commentaire créé</returns>
        /// <response code="201">Retourne le nouveau commentaire créé</response>
        [HttpPost]
        [ProducesResponseType(typeof(Comment), 201)]
        public async Task<ActionResult<Comment>> CreateComment([FromBody] Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCommentById), new { id = comment.CommentId }, comment);
        }

        /// <summary>
        /// Mettre à jour les informations d'un commentaire.
        /// </summary>
        /// <param name="id">ID du commentaire</param>
        /// <param name="comment">Le commentaire mis à jour</param>
        /// <returns>Le commentaire mis à jour</returns>
        /// <response code="200">Retourne le commentaire mis à jour</response>
        /// <response code="400">Si l'ID est invalide</response>
        /// <response code="404">Si le commentaire n'est pas trouvé</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Comment), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Comment>> UpdateComment(int id, [FromBody] Comment comment)
        {
            if (id != comment.CommentId)
            {
                return BadRequest("ID de commentaire invalide.");
            }

            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment == null)
            {
                return NotFound("Aucun commentaire trouvé pour l'ID: " + id);
            }

            existingComment.Content = comment.Content;
            existingComment.CreatedAt = comment.CreatedAt;

            await _context.SaveChangesAsync();

            return Ok(existingComment);
        }

        /// <summary>
        /// Supprimer un commentaire.
        /// </summary>
        /// <param name="id">ID du commentaire</param>
        /// <response code="204">Si le commentaire est supprimé avec succès</response>
        /// <response code="404">Si le commentaire n'est pas trouvé</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound("Aucun commentaire trouvé avec l'ID: " + id);
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
