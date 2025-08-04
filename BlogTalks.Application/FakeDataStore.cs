using BlogTalks.Domain.Entities;

namespace BlogTalks.Application
{
    public class FakeDataStore
    {

        public List<Comment> _comments;
        public List<BlogPost> _blogposts;
        public FakeDataStore()
        {
            _comments = new List<Comment>
            {
                new Comment {Id = 1,Text= "Fist comment on fist blog post!",CreatedBy = 1, CreatedAt = DateTime.Now, BlogPostID = 1 },
                new Comment {Id = 2,Text= "Second comment on fist blog post!",CreatedBy = 1, CreatedAt = DateTime.Now, BlogPostID = 1 },
                new Comment {Id = 3,Text= "Fist comment on second blogpost!",CreatedBy = 5, CreatedAt = DateTime.Now, BlogPostID = 2 },
            };
            _blogposts = new List<BlogPost>
            {
                new BlogPost
                {
                    Id = 1,
                    Title = "First title",
                    Text = "First blogpost!",
                    CreatedBy = 1,
                    CreatedAt = DateTime.Now,
                    Tags = new List<string> { "Literature", "Music" },
                    Comments = new List<Comment>
                    {
                        new Comment
                        {
                            Id = 1,
                            Text = "Great post!",
                            CreatedBy = 2,
                            CreatedAt = DateTime.Now,
                            BlogPostID = 1
                        },
                        new Comment
                        {
                            Id = 2,
                            Text = "Thanks for sharing!",
                            CreatedBy = 3,
                            CreatedAt = DateTime.Now,
                            BlogPostID = 1
                        }
                    }
                },
                new BlogPost
                {
                    Id = 2,
                    Title = "Second title",
                    Text = "Second blogpost!",
                    CreatedBy = 2,
                    CreatedAt = DateTime.Now,
                    Tags = new List<string> { "Mathematics", "Science" },
                    Comments = new List<Comment>
                    {
                        new Comment
                        {
                            Id = 3,
                            Text = "I love math!",
                            CreatedBy = 4,
                            CreatedAt = DateTime.Now,
                            BlogPostID = 2
                        },
                        new Comment
                        {
                            Id = 4,
                            Text = "Very informative!",
                            CreatedBy = 5,
                            CreatedAt = DateTime.Now,
                            BlogPostID = 2
                        }
                    }
                }
            };
        }
        //public async Task<Comment> GetCommentsById(int id)
        //{
        //   return await Task.FromResult(_comments.FirstOrDefault(c => c.Id == id));
        //}
        public Comment GetCommentsById(int id)
        {
            return _comments.FirstOrDefault(c => c.Id == id);
        }
        public async Task AddComment(Comment comment)
        {
            _comments.Add(comment);
             await Task.CompletedTask;
        }
        public Task DeleteComment(int id)
        {
            var comment = _comments.FirstOrDefault(c => c.Id == id);
            _comments.Remove(comment);
            return  Task.FromResult(comment);
        }
        public async Task<Comment> UpdateComment(Comment comment)
        {
            var existingComment = _comments.FirstOrDefault(c => c.Id == comment.Id);

            return await Task.FromResult(comment);

        }

        public BlogPost GetBlogPostById(int id)
        {
            return _blogposts.FirstOrDefault(b => b.Id == id);
        }
        public async Task AddBlogPost(BlogPost blogPost)
        {
            _blogposts.Add(blogPost);
            await Task.CompletedTask;
        }
        public Task DeleteBlogPost(int id)
        {
            var blogPost = _blogposts.FirstOrDefault(b => b.Id == id);
            _blogposts.Remove(blogPost);
            return Task.FromResult(blogPost);
        }
        public async Task<BlogPost> UpdateBlogPost(BlogPost blogPost)
        {
            var existingBlogPost = _blogposts.FirstOrDefault(b => b.Id == blogPost.Id);
            return await Task.FromResult(blogPost);
        }
       
    }
}
