/*
 * Auteurs: Mohamed Ali Bachar et Wasim Bessaou
 * Fichier: Class.cs
 * Projet: API_1
 * Date de création: 2024-05-31
 * Description: Ce fichier contient la classe DataContext qui permet de gérer la connexion à la base de données.
 */

using API_1.Models;
using Microsoft.EntityFrameworkCore;

namespace API_1.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
