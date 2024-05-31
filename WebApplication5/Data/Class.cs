using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;

namespace WebApplication5.Data
{
    //je cree une class et lui fait heriter du entity framework
    public class DataContext : DbContext
    {        
        //constructeur... ???
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        //methode pour la table...
        public DbSet<Etudiant> Etudiants { get; set; }
        public DbSet<Auteur> Auteurs { get; set; }
        public DbSet<Livre> Livres { get; set; }
    }
}
