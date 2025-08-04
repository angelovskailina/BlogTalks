using MediatR;

namespace BlogTalks.Application.BlogPosts.Queries
{
    public class GetAllHandler : IRequestHandler<GetAllRequest, IEnumerable<GetAllResponse>>
    {
        private readonly FakeDataStore _fakeDataStore;

        public GetAllHandler(FakeDataStore fakeDataStore)
        {
            _fakeDataStore = fakeDataStore;
        }

        public async Task<IEnumerable<GetAllResponse>> Handle(GetAllRequest request, CancellationToken cancellationToken)
        {
            var blogposts = _fakeDataStore._blogposts;
            var response = blogposts.Select(b => new GetAllResponse
            {
                Id = b.Id,
                Title = b.Title,
                Text = b.Text,
                CreatedBy = b.CreatedBy,
                CreatedAt = b.CreatedAt,
                Tags = b.Tags,
                Comments = b.Comments,
            });
            return response;
        }
    }
}
