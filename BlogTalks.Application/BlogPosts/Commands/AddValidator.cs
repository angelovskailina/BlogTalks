using FluentValidation;

namespace BlogTalks.Application.BlogPosts.Commands
{
    public class AddValidator : AbstractValidator<AddRequest>
    {
        public AddValidator()
        {
            RuleFor(x => x.Text).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}
