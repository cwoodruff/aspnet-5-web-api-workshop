using Chinook.Domain.Converters;
using Chinook.Domain.ApiModels;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chinook.Domain.Entities
{
    public class Playlist : IConvertModel<Playlist, PlaylistApiModel>
    {
        public Playlist()
        {
            PlaylistTracks = new HashSet<PlaylistTrack>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }


        public virtual ICollection<PlaylistTrack> PlaylistTracks { get; set; }

        public PlaylistApiModel Convert() =>
            new()
            {
                Id = Id,
                Name = Name
            };

        public async Task<PlaylistApiModel> ConvertAsync() =>
            new()
            {
                Id = Id,
                Name = Name
            };
    }
}