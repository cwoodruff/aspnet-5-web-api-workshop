using ChinookASPNETWebAPI.Domain.ApiModels;
using FluentValidation;

namespace ChinookASPNETWebAPI.Domain.Validation
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