using BlogTalks.Domain.Entities;
using MediatR;

namespace BlogTalks.Application.BlogPosts.Commands
{
    public record UpdateRequest(int Id,string Title,string Text,int CreatedBy,DateTime CreatedAt,List<string> Tags, List<Comment> Comments) : IRequest<UpdateResponse>;
    
}
