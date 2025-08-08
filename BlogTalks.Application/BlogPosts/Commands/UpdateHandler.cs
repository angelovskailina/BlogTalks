using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.BlogPosts.Commands
{
    public class UpdateHandler : IRequestHandler<UpdateRequest, UpdateResponse>
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public UpdateHandler(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }
        public async Task<UpdateResponse> Handle(UpdateRequest request, CancellationToken cancellationToken)
        {
            var blogPost = _blogPostRepository.GetById(request.Id);
            if (blogPost == null)
            {
                return null;
            }
            blogPost.Title = request.Title;
            blogPost.Text = request.Text;
            blogPost.Tags = request.Tags;

            _blogPostRepository.Update(blogPost);

            return new UpdateResponse { Id = blogPost.Id };
        }
    }
}
