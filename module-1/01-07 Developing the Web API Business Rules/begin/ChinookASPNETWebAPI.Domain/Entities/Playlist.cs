using System.Collections.Generic;
using System.Threading.Tasks;
using ChinookASPNETWebAPI.Domain.ApiModels;
using ChinookASPNETWebAPI.Domain.Converters;

namespace ChinookASPNETWebAPI.Domain.Entities
{
    public class Playlist : IConvertModel<Playlist, PlaylistApiModel>
    {
        public Playlist()
        {
            PlaylistTracks = new HashSet<PlaylistTrack>();
        }

        public int Id { get; set; }
        public string Name { get; set; }


        public virtual ICollection<PlaylistTrack> PlaylistTracks { get; set; }

        public PlaylistApiModel Convert() =>
            new()
            {
                Id = Id,
                Name = Name
            };
    }
}