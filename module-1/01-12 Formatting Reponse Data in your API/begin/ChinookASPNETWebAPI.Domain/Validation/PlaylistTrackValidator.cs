using ChinookASPNETWebAPI.Domain.ApiModels;
using FluentValidation;

namespace ChinookASPNETWebAPI.Domain.Validation
{
    public class PlaylistTrackValidator : AbstractValidator<PlaylistTrackApiModel>
    {
        public PlaylistTrackValidator()
        {
            RuleFor(plt => plt.PlaylistId).NotNull();
            RuleFor(plt => plt.TrackId).NotNull();
        }
    }
}