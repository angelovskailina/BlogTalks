using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.Comments.Queries
{
    public class GetAllByBlogPostIdHandler : IRequestHandler<GetAllByBlogPostIdRequest, List<GetAllByBlogPostIdResponse>>
    {
        private readonly ICommentRepository _commentRepository;

        public GetAllByBlogPostIdHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public Task<List<GetAllByBlogPostIdResponse>> Handle(GetAllByBlogPostIdRequest request, CancellationToken cancellationToken)
        {
            var comments = _commentRepository.GetCommentsByBlogPostId(request.blogPostId);

            if (comments == null)
            {
                return null;
            }

            var response = comments.Select(comments => new GetAllByBlogPostIdResponse
            {
                Id = comments.Id,
                Text = comments.Text,
                CreatedAt = comments.CreatedAt,
                CreatedBy = comments.CreatedBy,
                BlogPostId = comments.BlogPostID,
            }).ToList();

            return Task.FromResult(response);
        }
    }
}
