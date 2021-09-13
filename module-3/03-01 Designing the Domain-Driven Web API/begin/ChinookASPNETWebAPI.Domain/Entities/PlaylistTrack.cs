using ChinookASPNETWebAPI.Domain.ApiModels;
using ChinookASPNETWebAPI.Domain.Converters;

namespace ChinookASPNETWebAPI.Domain.Entities
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
    }
}