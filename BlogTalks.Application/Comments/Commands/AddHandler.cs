using BlogTalks.Domain.Entities;
using MediatR;

namespace BlogTalks.Application.Comments.Commands
{
    public class AddHandler : IRequestHandler<AddRequest,AddResponse>
    {
        private readonly FakeDataStore _fakeDataStore;

        public AddHandler(FakeDataStore fakeDataStore)
        {
            _fakeDataStore = fakeDataStore;
        }

        public async Task<AddResponse> Handle(AddRequest request, CancellationToken cancellationToken)
        {
            var comment = new Comment
            {
                Id = request.response.Id,
                Text = request.response.Text,
                CreatedAt = request.response.CreatedAt,
                CreatedBy = request.response.CreatedBy,
                BlogPostID = request.response.BlogPostID
            };
            await _fakeDataStore.AddComment(comment);
            return request.response;
        }
    }
}
