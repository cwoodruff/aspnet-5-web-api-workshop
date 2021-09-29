using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Chinook.Domain.Converters;
using Chinook.Domain.Entities;

namespace Chinook.Domain.ApiModels
{
    public class PlaylistApiModel : IConvertModel<PlaylistApiModel, Playlist>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore] public IList<TrackApiModel> Tracks { get; set; }

        [JsonIgnore] public IList<PlaylistTrackApiModel> PlaylistTracks { get; set; }

        public Playlist Convert() =>
            new()
            {
                Id = Id,
                Name = Name
            };

        public async Task<Playlist> ConvertAsync() =>
            new()
            {
                Id = Id,
                Name = Name
            };
    }
}