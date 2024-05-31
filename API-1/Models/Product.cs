/*
 * Auteurs: Mohamed Ali Bachar et Wasim Bessaou
 * Fichier: Product.cs
 * Projet: API_1
 * Date de création: 2024-05-31
 * Description: Ce fichier contient la classe Product qui représente un produit dans le système.
 */

namespace API_1.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
