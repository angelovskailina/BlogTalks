using BlogTalks.Domain.Exceptions;
using BlogTalks.Domain.Repositories;
using BlogTalks.Domain.Shared;
using MediatR;
using System.Net;

namespace BlogTalks.Application.Users.Commands
{
    public class LoginHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly IUserRepository _userRepository;

        public LoginHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetUsername(request.Username);
            if (user == null)
            {
                throw new BlogTalksException("Invalid username",HttpStatusCode.Unauthorized);
            }
            if (!PasswordHasher.VerifyPassword(request.Password, user.Password))
            {
                throw new BlogTalksException("Unsuccessful", HttpStatusCode.Unauthorized);
            }
            _userRepository.Login(request.Username, request.Password);
            return new LoginResponse("", "");
        }
    }
}
