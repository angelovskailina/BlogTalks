namespace BlogTalks.Application.Comments.Queries
{
    public class GetAllByBlogPostIdResponse
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int BlogPostId { get; set; }
    }
}
