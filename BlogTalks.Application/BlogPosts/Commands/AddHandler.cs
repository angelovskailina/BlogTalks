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
            int? userIdValue = int.TryParse(userIdClaim?.Value, out var parsedUserId) ? parsedUserId : null;

            var blogPost = new BlogPost
            {
                Title = request.Title,
                Text = request.Text,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = userIdValue.Value,
                Tags = request.Tags,

            };
            _blogPostRepository.Add(blogPost);
            return new AddResponse { Id = blogPost.Id };
        }
    }
}
