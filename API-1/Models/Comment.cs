namespace API_1.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Text { get; set; }
        // Autres propriétés nécessaires
    }

}
