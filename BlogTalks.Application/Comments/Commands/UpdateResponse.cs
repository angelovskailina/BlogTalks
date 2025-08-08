namespace BlogTalks.Application.Comments.Commands
{
    public class UpdateResponse
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int BlogPostID { get; set; }
    }
}
