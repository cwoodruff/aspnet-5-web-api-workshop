using Chinook.Domain.Converters;
using Chinook.Domain.ApiModels;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chinook.Domain.Entities
{
    public class Artist : IConvertModel<Artist, ArtistApiModel>
    {
        public Artist()
        {
            Albums = new HashSet<Album>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Album> Albums { get; set; }

        public ArtistApiModel Convert() =>
            new()
            {
                Id = Id,
                Name = Name
            };

        public async Task<ArtistApiModel> ConvertAsync() =>
            new()
            {
                Id = Id,
                Name = Name
            };
    }
}