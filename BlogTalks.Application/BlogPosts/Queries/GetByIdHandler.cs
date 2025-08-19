using BlogTalks.Domain.Exceptions;
using BlogTalks.Domain.Repositories;
using MediatR;
using System.Net;

namespace BlogTalks.Application.BlogPosts.Queries
{
    public class GetByIdHandler : IRequestHandler<GetByIdRequest, GetByIdResponse>
    {
        private readonly IBlogPostRepository _blogPostRepository;
        public GetByIdHandler(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        public async Task<GetByIdResponse> Handle(GetByIdRequest request, CancellationToken cancellationToken)
        {
            var blogPost = _blogPostRepository.GetById(request.Id);
            if (blogPost == null)
            {
                throw new BlogTalksException($"Blog post with Id {request.Id} not found.", HttpStatusCode.NotFound);
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
