using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Chinook.Domain.Converters;
using Chinook.Domain.ApiModels;

namespace Chinook.Domain.Entities
{
    public class PlaylistTrack : IConvertModel<PlaylistTrack, PlaylistTrackApiModel>
    {
        public int PlaylistId { get; set; }
        public int TrackId { get; set; }
        public virtual Playlist Playlist { get; set; }
        public virtual Track Track { get; set; }

        public PlaylistTrackApiModel Convert() =>
            new()
            {
                PlaylistId = PlaylistId,
                TrackId = TrackId
            };

        public async Task<PlaylistTrackApiModel> ConvertAsync() =>
            new()
            {
                PlaylistId = PlaylistId,
                TrackId = TrackId
            };
    }
}