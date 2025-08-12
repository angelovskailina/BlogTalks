using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BlogTalks.Application.BlogPosts.Commands
{
    public class AddHandler : IRequestHandler<AddRequest, AddResponse>
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddHandler(IBlogPostRepository blogPostRepository, IHttpContextAccessor httpContextAccessor)
        {
            _blogPostRepository = blogPostRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AddResponse> Handle(AddRequest request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            var userId = userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
        
            var blogPost = new BlogPost
            {
                Title = request.Title,
                Text = request.Text,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = userId,
                Tags = request.Tags,

            };
            _blogPostRepository.Add(blogPost);
            return new AddResponse { Id = blogPost.Id };
        }
    }
}
