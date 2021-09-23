using ChinookASPNETWebAPI.Domain.ApiModels;
using FluentValidation;

namespace ChinookASPNETWebAPI.Domain.Validation
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