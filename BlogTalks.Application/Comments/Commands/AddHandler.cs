using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BlogTalks.Application.Comments.Commands
{
    public class AddHandler : IRequestHandler<AddRequest, AddResponse>
    {
        private readonly IBlogPostRepository _blogPostRepository;
        public readonly ICommentRepository _commentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddHandler(IBlogPostRepository blogPostRepository, ICommentRepository commentRepository, IHttpContextAccessor httpContextAccessor)
        {
            _blogPostRepository = blogPostRepository;
            _commentRepository = commentRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AddResponse> Handle(AddRequest request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            var userId = userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;

            var blogPost = _blogPostRepository.GetById(request.blogPostId);
            if (blogPost == null)
            {
                return null;
            }
            var comment = new Comment
            {
                BlogPostID = request.blogPostId,
                BlogPost = blogPost,
                Text = request.Text,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = userId,
            };
            _commentRepository.Add(comment);
            return new AddResponse { Id = comment.Id };
        }
    }
}
