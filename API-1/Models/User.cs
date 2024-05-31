/*
 * Auteurs: Mohamed Ali Bachar et Wasim Bessaou
 * Fichier: User.cs
 * Projet: API_1
 * Date de création: 2024-05-31
 * Description: Ce fichier contient la classe User qui représente un utilisateur dans le système.
 */

namespace API_1.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
