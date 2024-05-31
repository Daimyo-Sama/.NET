using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuteursController : ControllerBase
    {
        private readonly DataContext _context;

        public AuteursController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Auteur>>> GetAuteurs()
        {
            return Ok(await _context.Auteurs.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Auteur>> GetAuteur(int id)
        {
            var auteur = await _context.Auteurs.FindAsync(id);
            if (auteur == null)
            {
                return NotFound("Aucun Auteur Trouvé avec l'ID: " + id);
            }
            return Ok(auteur);
        }

        [HttpPost]
        public async Task<IActionResult> AddAuteur(Auteur nouveauAuteur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Auteurs.Add(nouveauAuteur);
            await _context.SaveChangesAsync();
            return Ok(await _context.Auteurs.ToListAsync());
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<List<Auteur>>> UpdateAuteur(int id, Auteur auteur)
        {
            var auteurData = await _context.Auteurs.FindAsync(id);
            if (auteurData == null)
            {
                return NotFound("Aucun Auteur Correspondant trouvé pour l'ID: " + id);
            }

            auteurData.Nom = auteur.Nom;
            auteurData.Prenom = auteur.Prenom;
            auteurData.DateDeNaissance = auteur.DateDeNaissance;

            await _context.SaveChangesAsync();
            return Ok(await _context.Auteurs.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Auteur>>> DeleteAuteur(int id)
        {
            var auteur = await _context.Auteurs.FindAsync(id);
            if (auteur == null)
            {
                return NotFound("Aucun Auteur Trouvé avec l'ID: " + id);
            }

            _context.Auteurs.Remove(auteur);
            await _context.SaveChangesAsync();
            return Ok(await _context.Auteurs.ToListAsync());
        }
    }
}
