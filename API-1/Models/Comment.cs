/*
 * Auteurs: Mohamed Ali Bachar et Wasim Bessaou
 * Fichier: Comment.cs
 * Projet: API_1
 * Date de création: 2024-05-31
 * Description: Ce fichier contient la classe Comment qui représente un commentaire sur un produit.
 */

namespace API_1.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
