using System.Collections.Generic;
using ChinookASPNETWebAPI.Domain.Converters;
using ChinookASPNETWebAPI.Domain.Entities;

namespace ChinookASPNETWebAPI.Domain.ApiModels
{
    public class PlaylistApiModel : IConvertModel<PlaylistApiModel, Playlist>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<TrackApiModel> Tracks { get; set; }

        public IList<PlaylistTrackApiModel> PlaylistTracks { get; set; }

        public Playlist Convert() =>
            new()
            {
                Id = Id,
                Name = Name
            };
    }
}