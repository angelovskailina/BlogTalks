using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.Comments.Commands
{
    public class DeleteHandler : IRequestHandler<DeleteRequest, DeleteResponse>
    {
        public readonly ICommentRepository _commentRepository;
        public DeleteHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public async Task<DeleteResponse> Handle(DeleteRequest request, CancellationToken cancellationToken)
        {
            var comment = _commentRepository.GetById(request.id);
            if (comment == null)
            {
                return null;
            }
            _commentRepository.Delete(comment);

            return new DeleteResponse();
        }
    }
}
