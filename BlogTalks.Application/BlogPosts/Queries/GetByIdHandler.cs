using MediatR;

namespace BlogTalks.Application.BlogPosts.Queries
{
    public class GetByIdHandler : IRequestHandler<GetByIdRequest, GetByIdResponse>
    {
        private readonly FakeDataStore _fakeDataStore;

        public GetByIdHandler(FakeDataStore fakeDataStore)
        {
            _fakeDataStore = fakeDataStore;
        }

        public async Task<GetByIdResponse> Handle(GetByIdRequest request, CancellationToken cancellationToken)
        {
            var blogPost = _fakeDataStore.GetBlogPostById(request.id);
            if (blogPost == null)
            {
                throw new Exception("The blogpost is null");
            }
            return new GetByIdResponse
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Text = blogPost.Text,
                CreatedAt = blogPost.CreatedAt,
                CreatedBy = blogPost.CreatedBy,
                Tags = blogPost.Tags,
                Comments = blogPost.Comments,
            };
           
        }
    }
}
