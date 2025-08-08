using BlogTalks.Domain.Repositories;
using BlogTalks.Domain.Shared;
using MediatR;

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
                throw new Exception("Invalid username");
            }

            if (!PasswordHasher.VerifyPassword(request.Password, user.Password))
            {
                throw new Exception("Invalid password");
            }
            _userRepository.Login(request.Username, request.Password);
            return new LoginResponse("", "");
        }
    }
}
