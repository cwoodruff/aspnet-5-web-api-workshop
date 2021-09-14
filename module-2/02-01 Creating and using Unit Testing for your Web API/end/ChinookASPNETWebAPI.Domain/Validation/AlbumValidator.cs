using ChinookASPNETWebAPI.Domain.ApiModels;
using FluentValidation;

namespace ChinookASPNETWebAPI.Domain.Validation
{
    public class AlbumValidator : AbstractValidator<AlbumApiModel>
    {
        public AlbumValidator()
        {
            RuleFor(a => a.Title).NotNull();
            RuleFor(a => a.Title).MinimumLength(3);
            RuleFor(a => a.Title).MaximumLength(160);
            RuleFor(a => a.ArtistId).NotNull();
        }
    }
}