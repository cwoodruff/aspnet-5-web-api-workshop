using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Chinook.Domain.Converters;
using Chinook.Domain.Entities;

namespace Chinook.Domain.ApiModels
{
    public class PlaylistTrackApiModel : IConvertModel<PlaylistTrackApiModel, PlaylistTrack>
    {
        public int PlaylistId { get; set; }
        public int TrackId { get; set; }

        [JsonIgnore] public PlaylistApiModel Playlist { get; set; }

        [JsonIgnore] public TrackApiModel Track { get; set; }

        public PlaylistTrack Convert() =>
            new()
            {
                PlaylistId = PlaylistId,
                TrackId = TrackId
            };

        public async Task<PlaylistTrack> ConvertAsync() =>
            new()
            {
                PlaylistId = PlaylistId,
                TrackId = TrackId
            };
    }
}