using MediatR;

namespace BlogTalks.Application.BlogPosts.Commands
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
            var blogPost = _fakeDataStore.GetBlogPostById(request.Id);
            if (blogPost == null)
            {
                throw new Exception("BlogPost is null");
            }
            blogPost.Id = request.Id;
            blogPost.Title = request.Title;
            blogPost.Text = request.Text;
            blogPost.CreatedAt = request.CreatedAt;
            blogPost.CreatedBy = request.CreatedBy;
            blogPost.Tags = request.Tags;
            blogPost.Comments = request.Comments;

            var updatedBlogPost = await _fakeDataStore.UpdateBlogPost(blogPost);

            return new UpdateResponse
            {
                Id = updatedBlogPost.Id,
                Title = updatedBlogPost.Title,
                Text = updatedBlogPost.Text,
                CreatedAt = updatedBlogPost.CreatedAt,
                CreatedBy = updatedBlogPost.CreatedBy,
                Tags = updatedBlogPost.Tags,
                Comments = updatedBlogPost.Comments,
            };
        }
    }
}
