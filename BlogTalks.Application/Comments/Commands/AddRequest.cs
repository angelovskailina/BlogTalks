using MediatR;

namespace BlogTalks.Application.Comments.Commands
{
    public record AddRequest(AddResponse response) : IRequest<AddResponse>;
    
   
}
