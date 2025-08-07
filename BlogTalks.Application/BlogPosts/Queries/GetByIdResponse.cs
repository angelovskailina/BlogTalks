using BlogTalks.Domain.Entities;

namespace BlogTalks.Application.BlogPosts.Queries
{
    public class GetByIdResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> Tags { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
