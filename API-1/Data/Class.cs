using API_1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace API_1.Data
{
    //je cree une class et lui fait heriter du entity framework
    public class DataContext : DbContext
    {
        //constructeur... ???
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        //methode pour la table...
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
