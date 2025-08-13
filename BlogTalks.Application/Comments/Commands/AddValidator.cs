using FluentValidation;

namespace BlogTalks.Application.Comments.Commands
{
    public class AddValidator : AbstractValidator<AddRequest>
    {
        public AddValidator()
        {
            RuleFor(x => x.Text).NotEmpty();
        }
    }
}
