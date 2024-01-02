using Crochet.Application.Models;
using FluentValidation;

namespace Crochet.Application.Validators;

public class PostValidator : AbstractValidator<Post>
{
    public PostValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
        RuleFor(x => x.Rating).NotEmpty().WithMessage("Rating is required.");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required.");
        RuleFor(x => x.Rating)
            .LessThanOrEqualTo(5)
            .WithMessage("Rating must be less than or equal to 5.");
    }
}
