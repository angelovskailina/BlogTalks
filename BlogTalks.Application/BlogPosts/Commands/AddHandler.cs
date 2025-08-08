using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.BlogPosts.Commands
{
    public class AddHandler : IRequestHandler<AddRequest, AddResponse>
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public AddHandler(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }
        public async Task<AddResponse> Handle(AddRequest request, CancellationToken cancellationToken)
        {
            var blogPost = new BlogPost
            {
                Title = request.Title,
                Text = request.Text,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = 55,
                Tags = request.Tags,

            };
            _blogPostRepository.Add(blogPost);
            return new AddResponse { Id = blogPost.Id };
        }
    }
}
