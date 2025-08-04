using MediatR;

namespace BlogTalks.Application.BlogPosts.Commands
{
    public class DeleteHandler : IRequestHandler<DeleteRequest, DeleteResponse>
    {
        private readonly FakeDataStore _fakeDataStore;

        public DeleteHandler(FakeDataStore fakeDataStore)
        {
            _fakeDataStore = fakeDataStore;
        }

        public async Task<DeleteResponse> Handle(DeleteRequest request, CancellationToken cancellationToken)
        {
            var blogPost = _fakeDataStore.GetBlogPostById(request.id);
            if (blogPost == null)
            {
                throw new Exception("BlogpPost is null.");
            }
            await _fakeDataStore.DeleteBlogPost(blogPost.Id);
            return new DeleteResponse();
        }
    }
}
