using BlogTalks.Domain.Entities;
using MediatR;

namespace BlogTalks.Application.BlogPosts.Commands
{
    public class AddHandler : IRequestHandler<AddRequest, AddResponse>
    {
        private readonly FakeDataStore _fakeDataStore;

        public AddHandler(FakeDataStore fakeDataStore)
        {
            _fakeDataStore = fakeDataStore;
        }

        public async Task<AddResponse> Handle(AddRequest request, CancellationToken cancellationToken)
        { 
            var blogPost = new BlogPost
            {
                Id = request.response.Id,
                Title = request.response.Title,
                Text = request.response.Text,
                CreatedAt = request.response.CreatedAt,
                CreatedBy = request.response.CreatedBy,
                Tags = request.response.Tags,
                Comments = request.response.Comments,
            };
            await _fakeDataStore.AddBlogPost(blogPost);
            return request.response;
        }
        
    }
}
