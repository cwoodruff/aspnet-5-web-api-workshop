using Chinook.Domain.ApiModels;
using FluentValidation;

namespace Chinook.Domain.Validation
{
    public class PlaylistValidator : AbstractValidator<PlaylistApiModel>
    {
        public PlaylistValidator()
        {
            RuleFor(p => p.Name).NotNull();
            RuleFor(p => p.Name).MaximumLength(120);
        }
    }
}