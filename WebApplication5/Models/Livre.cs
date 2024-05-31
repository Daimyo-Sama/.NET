namespace WebApplication5.Models
{
    public class Livre
    {
        public int Id { get; set; }
        public string? Titre { get; set; }
        public string? Genre { get; set; }
        public int AnneeDePublication { get; set; }
        public int AuteurId { get; set; }
        public Auteur Auteur { get; set; }
    }
}
