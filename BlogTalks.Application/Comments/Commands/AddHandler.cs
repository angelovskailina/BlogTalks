using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using System.Security.Claims;

namespace BlogTalks.Application.Comments.Commands
{
    public class AddHandler : IRequestHandler<AddRequest, AddResponse>
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUserRepository _userRepository;

        public AddHandler(IBlogPostRepository blogPostRepository, ICommentRepository commentRepository, IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IUserRepository userRepository)
        {
            _blogPostRepository = blogPostRepository;
            _commentRepository = commentRepository;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
            _userRepository = userRepository;
        }

        public async Task<AddResponse> Handle(AddRequest request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            int? userIdValue = int.TryParse(userIdClaim?.Value, out var parsedUserId) ? parsedUserId : null;

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
                CreatedBy = userIdValue.Value,
            };
            _commentRepository.Add(comment);

            var httpClient = _httpClientFactory.CreateClient("EmailSenderApi");
            var blogpostCreator = _userRepository.GetById(blogPost.CreatedBy);
            var commentCreator = _userRepository.GetById(userIdValue.Value);

            var dto = new
            {
                From = commentCreator.Email,
                To = blogpostCreator.Email,
                Subject = "New Comment Added",
                Body = $"A new comment has been added to the blog post '{blogPost.Title}' by user {commentCreator.Name}."
            };
            await httpClient.PostAsJsonAsync("/email", dto, cancellationToken);
            return new AddResponse { Id = comment.Id };
        }
    }
}
