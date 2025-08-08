using BlogTalks.Domain.Exceptions;
using BlogTalks.Domain.Repositories;
using BlogTalks.Domain.Shared;
using MediatR;
using System.Net;

namespace BlogTalks.Application.Users.Commands
{
    public class RegisterHandler : IRequestHandler<RegisterRequest, RegisterResponse>
    {
        private readonly IUserRepository _userRepository;
        public RegisterHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<RegisterResponse> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetUsername(request.Username);
            if (user != null)
            {
                throw new BlogTalksException("Username already in use!", HttpStatusCode.Unauthorized);
            }

            var registeredUser = _userRepository.Register(request.Username, request.Password,request.Name,request.Email);

            if (registeredUser == null)
            {
                throw new BlogTalksException("User not registered", HttpStatusCode.Unauthorized);
            }
            return new RegisterResponse(Message:"Registered user");
        }
    }
}
