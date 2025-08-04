using MediatR;

namespace BlogTalks.Application.Comments.Queries
{
    public class GetAllByBlogPostIdHandler : IRequestHandler<GetAllByBlogPostIdRequest, List<GetAllByBlogPostIdResponse>>
    {
        private readonly FakeDataStore _fakeDataStore;

        public GetAllByBlogPostIdHandler(FakeDataStore fakeDataStore) => _fakeDataStore = fakeDataStore;

        public Task<List<GetAllByBlogPostIdResponse>> Handle(GetAllByBlogPostIdRequest request, CancellationToken cancellationToken)
        {
            var blogPost = _fakeDataStore.GetBlogPostById(request.Id);

            if (blogPost == null)
            {
                throw new Exception("Blogpost doesn't exits");
            }

            var response = blogPost.Comments.Select(comments => new GetAllByBlogPostIdResponse
            {
                Id = comments.Id,
                Text = comments.Text,
                CreatedAt = comments.CreatedAt,
                CreatedBy = comments.CreatedBy,
                BlogPostId = blogPost.Id,
            }).ToList();

            return Task.FromResult(response);
        }
    }
}
