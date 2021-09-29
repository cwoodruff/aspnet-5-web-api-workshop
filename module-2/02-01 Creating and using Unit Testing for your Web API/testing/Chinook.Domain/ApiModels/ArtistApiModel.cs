using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Chinook.Domain.Converters;
using Chinook.Domain.Entities;

namespace Chinook.Domain.ApiModels
{
    public class ArtistApiModel : IConvertModel<ArtistApiModel, Artist>
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [JsonIgnore] public IList<AlbumApiModel>? Albums { get; set; }

        public Artist Convert() =>
            new()
            {
                Id = Id,
                Name = Name ?? string.Empty
            };

        public async Task<Artist> ConvertAsync() =>
            new()
            {
                Id = Id,
                Name = Name ?? string.Empty
            };
    }
}