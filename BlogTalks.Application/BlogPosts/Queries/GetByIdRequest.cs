using MediatR;

namespace BlogTalks.Application.BlogPosts.Queries
{
    public record GetByIdRequest(int Id) : IRequest<GetByIdResponse>;
}
