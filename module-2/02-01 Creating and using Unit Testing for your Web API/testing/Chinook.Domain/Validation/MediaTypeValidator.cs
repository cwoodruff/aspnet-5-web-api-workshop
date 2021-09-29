using Chinook.Domain.ApiModels;
using FluentValidation;

namespace Chinook.Domain.Validation
{
    public class MediaTypeValidator : AbstractValidator<MediaTypeApiModel>
    {
        public MediaTypeValidator()
        {
            RuleFor(m => m.Name).NotNull();
            RuleFor(m => m.Name).MaximumLength(120);
        }
    }
}