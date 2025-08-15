using BlogTalks.Application.Contracts;
using BlogTalks.Domain.Entities;

namespace BlogTalks.Application.BlogPosts.Queries
{
    public class GetAllResponse
    {
        public List<BlogPostModel> BlogPosts { get; set; } = new List<BlogPostModel>();
        public Metadata Metadata { get; set; }
    }
}
