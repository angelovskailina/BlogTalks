using BlogTalks.Domain.Entities;

namespace BlogTalks.API.Dtos
{
    public class BlogPostDTO
    {
        public long Id { get; set; }
        public String Title { get; set; }
        public string Text { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<String> Tags { get; set; }
        public List<CommentDTO> Comments { get; set; }
    }
}
