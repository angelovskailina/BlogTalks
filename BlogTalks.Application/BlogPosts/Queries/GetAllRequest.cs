using MediatR;

namespace BlogTalks.Application.BlogPosts.Queries
{
    public record GetAllRequest : IRequest<IEnumerable<GetAllResponse>>;

}
