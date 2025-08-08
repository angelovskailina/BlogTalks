using MediatR;

namespace BlogTalks.Application.Users.Commands
{
    public record LoginRequest(string Username, string Password) : IRequest<LoginResponse>;
}
