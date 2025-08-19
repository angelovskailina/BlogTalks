using BlogTalks.Email.DTOs;

namespace BlogTalks.Email.Services
{
    public interface IEmailSender
    {
        Task Send(EmailDto request);
    }
}
