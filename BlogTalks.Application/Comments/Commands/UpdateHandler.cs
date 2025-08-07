using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.Comments.Commands
{
    public class UpdateHandler : IRequestHandler<UpdateRequest, UpdateResponse>
    {
        public readonly ICommentRepository _commentRepository;

        public UpdateHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<UpdateResponse> Handle(UpdateRequest request, CancellationToken cancellationToken)
        {
            var comment = _commentRepository.GetById(request.Id);

            if (comment == null)
            {
                return null;
            }
            comment.Text = request.Text;
            comment.CreatedAt = DateTime.UtcNow;

             _commentRepository.Update(comment);

            return new UpdateResponse
            {
                Id = comment.Id,
                Text = comment.Text,
                CreatedAt = comment.CreatedAt,
                CreatedBy = comment.CreatedBy,
                BlogPostID = comment.BlogPostID,
            };
            //return Task.FromResult(new UpdateResponse());

        }
    }
}
