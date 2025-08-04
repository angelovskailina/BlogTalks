using MediatR;

namespace BlogTalks.Application.Comments.Queries
{
    public class GetByIdHandler : IRequestHandler<GetByIdRequest, GetByIdResponse>
    {
        private readonly FakeDataStore _fakeDataStore;

        public GetByIdHandler(FakeDataStore fakeDataStore) => _fakeDataStore = fakeDataStore;

        public async Task<GetByIdResponse> Handle(GetByIdRequest request, CancellationToken cancellationToken)
        {
            var comment =  _fakeDataStore.GetCommentsById(request.id);

            if (comment == null)
            {
                throw new Exception("Comment is null.");
                //inicijalno treba da se frli Not Found() vo kontrolerot;
            }
            return new GetByIdResponse
            {
                Id = comment.Id,
                Text = comment.Text,
                CreatedAt = comment.CreatedAt,
                CreatedBy = comment.CreatedBy,
                BlogPostId = comment.BlogPostID
            };
        }
    }
}
