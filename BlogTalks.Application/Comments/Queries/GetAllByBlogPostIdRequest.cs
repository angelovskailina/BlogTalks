using MediatR;


namespace BlogTalks.Application.Comments.Queries
{
    public record GetAllByBlogPostIdRequest(int Id) : IRequest<List<GetAllByBlogPostIdResponse>>;
   
}
