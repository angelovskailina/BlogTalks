using BlogTalks.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BlogTalks.Application.BlogPosts.Commands
{
    public class UpdateHandler : IRequestHandler<UpdateRequest, UpdateResponse>
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateHandler(IBlogPostRepository blogPostRepository, IHttpContextAccessor httpContextAccessor)
        {
            _blogPostRepository = blogPostRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdateResponse> Handle(UpdateRequest request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            int loggedInUserId = int.Parse(userIdClaim.Value);

            var blogPost = _blogPostRepository.GetById(request.Id);
            if (blogPost == null)
            {
                return null;
            }

            if (blogPost.CreatedBy != loggedInUserId)
            {
                throw new UnauthorizedAccessException("You can only update your own blog posts.");
            }

            blogPost.Title = request.Title;
            blogPost.Text = request.Text;
            blogPost.Tags = request.Tags;

            _blogPostRepository.Update(blogPost);

            return new UpdateResponse { Id = blogPost.Id };
        }
    }
}
