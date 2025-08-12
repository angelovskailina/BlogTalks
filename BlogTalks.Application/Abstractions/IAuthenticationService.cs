using BlogTalks.Domain.Entities;

namespace BlogTalks.Application.Abstractions
{
    public interface IAuthenticationService
    {
        string CreateToken(User user);
    }
}
