using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivresController : ControllerBase
    {
        private readonly DataContext _context;

        public LivresController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Livre>>> GetLivres()
        {
            return Ok(await _context.Livres.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Livre>> GetLivre(int id)
        {
            var livre = await _context.Livres.FindAsync(id);
            if (livre == null)
            {
                return NotFound("Aucun Livre Trouvé avec l'ID: " + id);
            }
            return Ok(livre);
        }

        [HttpPost]
        public async Task<ActionResult<List<Livre>>> AddLivre(Livre nouveauLivre)
        {
            _context.Livres.Add(nouveauLivre);
            await _context.SaveChangesAsync();
            return Ok(await _context.Livres.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Livre>>> UpdateLivre(int id, Livre livre)
        {
            var livreData = await _context.Livres.FindAsync(id);
            if (livreData == null)
            {
                return NotFound("Aucun Livre Correspondant trouvé pour l'ID: " + id);
            }

            livreData.Titre = livre.Titre;
            livreData.Genre = livre.Genre;
            livreData.AnneeDePublication = livre.AnneeDePublication;
            livreData.AuteurId = livre.AuteurId;

            await _context.SaveChangesAsync();
            return Ok(await _context.Livres.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Livre>>> DeleteLivre(int id)
        {
            var livre = await _context.Livres.FindAsync(id);
            if (livre == null)
            {
                return NotFound("Aucun Livre Trouvé avec l'ID: " + id);
            }

            _context.Livres.Remove(livre);
            await _context.SaveChangesAsync();
            return Ok(await _context.Livres.ToListAsync());
        }
    }
}
