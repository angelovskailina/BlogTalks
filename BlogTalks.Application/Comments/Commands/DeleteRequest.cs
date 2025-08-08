using MediatR;

namespace BlogTalks.Application.Comments.Commands
{
    public record DeleteRequest(int id) : IRequest<DeleteResponse>;

}
