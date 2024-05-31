using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Models;

namespace WebApplication5.Controllers

{// Designe le nom du controller comme la route
    [ApiController]
    [Route("api/[controller]")]
    public class EtudiantsController : Controller
    {
        //on cree une propriete pour lire linstance de entity framework
        private readonly DataContext _context;

        //on lintegre au constructeur pour permettre sa creation
        public EtudiantsController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Etudiant>>> ObtenirEtudiants()
        {
            var etudiant = await _context.Etudiants.ToListAsync();

            //reponse dans le body
            return Ok(etudiant);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Etudiant>>> ObtenirEtudiant(int id)
        {
            var etudiant = await _context.Etudiants.FindAsync(id);

            //reponse dans le body
            return Ok(etudiant);
            if(etudiant == null)
            {
                return NotFound("Aucun Etudiant Trouver avec le id:" + id);
            }
        }
        [HttpPost]
        public async Task<ActionResult<List<Etudiant>>> AjouterEtudiant(Etudiant NouveauEtudiant)
        {
            _context.Etudiants.Add(NouveauEtudiant);
            await _context.SaveChangesAsync();
            return Ok(await _context.Etudiants.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<Etudiant>>> ModifierEtudiant(Etudiant Etudiant)
        {
            var etudiantData = await _context.Etudiants.FindAsync(Etudiant.Id);
            if(etudiantData == null)
            {
                return NotFound("Aucun Etudiant Correspondant");
            }

            etudiantData.Nom = Etudiant.Nom;
            etudiantData.Prenom = Etudiant.Prenom; 
            etudiantData.Matricule = Etudiant.Matricule;
            etudiantData.LieuNaissance = Etudiant.LieuNaissance;

            await _context.SaveChangesAsync();

            return Ok(await _context.Etudiants.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Etudiant>>> DeleteEtudiant(int id)
        {
            var etudiant = await _context.Etudiants.FindAsync(id);
            
            if (etudiant == null)
            {
                return NotFound("Aucun Etudiant Trouver avec le id:" + id);
            }
            
            _context.Etudiants.Remove(etudiant);

            await _context.SaveChangesAsync();
            return Ok(await _context.Etudiants.ToListAsync());
        }
    }
}
