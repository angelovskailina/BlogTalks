using BlogTalks.Application.Contracts;
using BlogTalks.Domain.Repositories;
using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.BlogPosts.Queries
{
    public class GetAllHandler : IRequestHandler<GetAllRequest, GetAllResponse>
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IUserRepository _userRepository;

        public GetAllHandler(IBlogPostRepository blogPostRepository, IUserRepository userRepository)
        {
            _blogPostRepository = blogPostRepository;
            _userRepository = userRepository;
        }

        public async Task<GetAllResponse> Handle(GetAllRequest request, CancellationToken cancellationToken)
        {
            var (totalCount, blogposts) = _blogPostRepository.GetAllBlogposts(
                request.PageNumber,
                request.PageSize,
                request.SearchWord,
                request.Tag);

            var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

            var userIds = blogposts.Select(bp => bp.CreatedBy).Distinct().ToList();
            var users = _userRepository.GetUsersByIds(userIds);
            var userDict = users.ToDictionary(u => u.Id, u => u.Username);

            var blogPostModels = blogposts.Select(b => new BlogPostModel
            {
                Id = b.Id,
                Title = b.Title,
                Text = b.Text,
                Tags = b.Tags,
                CreatorName = userDict.GetValueOrDefault(b.CreatedBy, string.Empty)
            }).ToList();

            return new GetAllResponse
            {
                BlogPosts = blogPostModels,
                Metadata = new Metadata
                {
                    TotalCount = totalCount,
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    TotalPages = totalPages
                }
            };
        }
    }
}
