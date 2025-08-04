using MediatR;

namespace BlogTalks.Application.Comments.Commands
{
    public class DeleteHandler : IRequestHandler<DeleteRequest,DeleteResponse>
    {
        private readonly FakeDataStore _fakeDataStore;

        public DeleteHandler(FakeDataStore fakeDataStore)
        {
            _fakeDataStore = fakeDataStore;
        }
        public async Task<DeleteResponse> Handle(DeleteRequest request, CancellationToken cancellationToken)
        {
            var comment = _fakeDataStore.GetCommentsById(request.id);
            if (comment == null)
            {
                throw new Exception("Comment is null.");
            }
            await _fakeDataStore.DeleteComment(comment.Id);

            return new DeleteResponse();
        }
    }
}
