using MediatR;

namespace BlogTalks.Application.Comments.Commands
{
    public record UpdateRequest(int Id, string Text,int CreatedBy,DateTime CreatedAt, int BlogPostId) : IRequest<UpdateResponse>;
   
}
