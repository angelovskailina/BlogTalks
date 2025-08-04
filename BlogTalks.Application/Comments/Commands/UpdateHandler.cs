using MediatR;

namespace BlogTalks.Application.Comments.Commands
{
    public class UpdateHandler : IRequestHandler<UpdateRequest, UpdateResponse>
    {
        private readonly FakeDataStore _fakeDataStore;

        public UpdateHandler(FakeDataStore fakeDataStore)
        {
            _fakeDataStore = fakeDataStore;
        }

        public async Task<UpdateResponse> Handle(UpdateRequest request, CancellationToken cancellationToken)
        {
            var comment = _fakeDataStore.GetCommentsById(request.Id);
            if (comment == null)
            {
                throw new Exception("Comment is null");
                //return null;
            }

            comment.Id = request.Id;
            comment.Text = request.Text;
            comment.CreatedAt = DateTime.UtcNow;
            comment.CreatedBy = request.CreatedBy;
            comment.BlogPostID = request.BlogPostId;

            var updatedComment = await _fakeDataStore.UpdateComment(comment);

            return new UpdateResponse
            {
                Id = updatedComment.Id,
                Text = updatedComment.Text,
                CreatedAt = updatedComment.CreatedAt,
                CreatedBy = updatedComment.CreatedBy,
                BlogPostID = updatedComment.BlogPostID,
            };
        }
    }
}
