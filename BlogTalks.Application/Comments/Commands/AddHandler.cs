using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.Comments.Commands
{
    public class AddHandler : IRequestHandler<AddRequest,AddResponse>
    {
        private readonly IBlogPostRepository _blogPostRepository;
        public readonly ICommentRepository _commentRepository;

        public AddHandler(IBlogPostRepository blogPostRepository, ICommentRepository commentRepository)
        {
            _blogPostRepository = blogPostRepository;
            _commentRepository = commentRepository;
        }

        public async Task<AddResponse> Handle(AddRequest request, CancellationToken cancellationToken)
        {
            var blogPost = _blogPostRepository.GetById(request.blogPostId);
            if (blogPost == null)
            {
                return null;
            }
            var comment = new Comment
            {
                BlogPostID = request.blogPostId,
                BlogPost = blogPost,
                Text = request.Text,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = 5, //not implemented 
            };
            _commentRepository.Add(comment);
            return new AddResponse { Id = comment.Id };
           // return new AddResponse{ Id = comment.Id };
        }
    }
}
