namespace culinaryApp.Models
{
    public class UserComment
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string? CommentText { get; set; }
        public User? User { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
