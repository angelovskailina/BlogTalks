using MediatR;


namespace BlogTalks.Application.Comments.Queries
{
    public record GetAllByBlogPostIdRequest(int blogPostId) : IRequest<List<GetAllByBlogPostIdResponse>>;

}
