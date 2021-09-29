using Chinook.Domain.ApiModels;
using FluentValidation;

namespace Chinook.Domain.Validation
{
    public class GenreValidator : AbstractValidator<GenreApiModel>
    {
        public GenreValidator()
        {
            RuleFor(g => g.Name).NotNull();
            RuleFor(g => g.Name).MaximumLength(120);
        }
    }
}