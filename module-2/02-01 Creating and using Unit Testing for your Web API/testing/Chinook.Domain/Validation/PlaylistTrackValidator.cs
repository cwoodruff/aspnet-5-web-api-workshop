using Chinook.Domain.ApiModels;
using FluentValidation;

namespace Chinook.Domain.Validation
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