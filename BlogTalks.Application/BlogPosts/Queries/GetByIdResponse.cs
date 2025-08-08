using BlogTalks.Domain.Entities;

namespace BlogTalks.Application.BlogPosts.Queries
{
    public class GetByIdResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
