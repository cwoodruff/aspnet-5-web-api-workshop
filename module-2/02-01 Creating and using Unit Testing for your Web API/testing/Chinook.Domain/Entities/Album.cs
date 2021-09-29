using Chinook.Domain.Converters;
using Chinook.Domain.ApiModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chinook.Domain.Entities
{
    public class Album : IConvertModel<Album, AlbumApiModel>
    {
        public Album()
        {
            Tracks = new HashSet<Track>();
        }

        [Key] public int Id { get; set; }
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

        public async Task<AlbumApiModel> ConvertAsync() =>
            new()
            {
                Id = Id,
                ArtistId = ArtistId,
                Title = Title
            };
    }
}