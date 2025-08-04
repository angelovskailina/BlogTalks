using MediatR;

namespace BlogTalks.Application.BlogPosts.Commands
{
    public record AddRequest(AddResponse response) : IRequest<AddResponse>;
   
}
