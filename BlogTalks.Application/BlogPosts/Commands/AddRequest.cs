using MediatR;

namespace BlogTalks.Application.BlogPosts.Commands
{
    public record AddRequest(string Title, string Text, List<string> Tags) : IRequest<AddResponse>;
}
