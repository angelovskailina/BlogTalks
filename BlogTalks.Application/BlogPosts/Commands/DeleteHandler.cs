using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.BlogPosts.Commands
{
    public class DeleteHandler : IRequestHandler<DeleteRequest, DeleteResponse>
    {
        private readonly IBlogPostRepository _blogPostRepository;
        public DeleteHandler(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }
        public async Task<DeleteResponse> Handle(DeleteRequest request, CancellationToken cancellationToken)
        {
            var blogPost = _blogPostRepository.GetById(request.id);
            if (blogPost == null)
            {
                return null;
            }
            _blogPostRepository.Delete(blogPost);
            return new DeleteResponse();
        }
    }
}
