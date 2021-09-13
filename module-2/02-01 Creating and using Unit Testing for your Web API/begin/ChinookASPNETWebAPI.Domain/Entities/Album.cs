using System.Collections.Generic;
using ChinookASPNETWebAPI.Domain.ApiModels;
using ChinookASPNETWebAPI.Domain.Converters;

#nullable disable

namespace ChinookASPNETWebAPI.Domain.Entities
{
    public partial class Album : IConvertModel<Album, AlbumApiModel>
    {
        public Album()
        {
            Tracks = new HashSet<Track>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int ArtistId { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual ICollection<Track> Tracks { get; set; }

        public AlbumApiModel Convert() =>
            new()
            {
                Id = Id,
                ArtistId = ArtistId,
                Title = Title
            };
    }
}
