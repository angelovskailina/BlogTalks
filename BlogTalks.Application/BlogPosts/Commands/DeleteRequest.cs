using MediatR;

namespace BlogTalks.Application.BlogPosts.Commands
{
    public record DeleteRequest(int id) : IRequest<DeleteResponse>;
    
}
