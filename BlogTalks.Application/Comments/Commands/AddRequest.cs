using MediatR;

namespace BlogTalks.Application.Comments.Commands
{
    public record AddRequest(string Text, int blogPostId) : IRequest<AddResponse>;
    
   
}
