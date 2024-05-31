/*
 * Auteurs: Mohamed Ali Bachar et Wasim Bessaou
 * Fichier: Order.cs
 * Projet: API_1
 * Date de création: 2024-05-31
 * Description: Ce fichier contient la classe Order qui représente une commande passée par un utilisateur.
 */

namespace API_1.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
