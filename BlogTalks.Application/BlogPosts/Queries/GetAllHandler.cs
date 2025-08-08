using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.BlogPosts.Queries
{
    public class GetAllHandler : IRequestHandler<GetAllRequest, IEnumerable<GetAllResponse>>
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public GetAllHandler(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }
        public async Task<IEnumerable<GetAllResponse>> Handle(GetAllRequest request, CancellationToken cancellationToken)
        {
            var blogposts = _blogPostRepository.GetAll();
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
