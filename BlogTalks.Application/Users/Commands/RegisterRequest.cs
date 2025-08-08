using MediatR;

namespace BlogTalks.Application.Users.Commands
{
    public record RegisterRequest(string Username, string Name, string Password,string Email) : IRequest<RegisterResponse>;


}
