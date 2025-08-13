using FluentValidation;

namespace BlogTalks.Application.Users.Commands
{
    public class RegisterValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("Minimum 8 characters");
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}
