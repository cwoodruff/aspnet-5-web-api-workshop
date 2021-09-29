using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Chinook.Domain.Converters;
using Chinook.Domain.Entities;

namespace Chinook.Domain.ApiModels
{
    public class AlbumApiModel : IConvertModel<AlbumApiModel, Album>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ArtistId { get; set; }
        public string? ArtistName { get; set; }

        [JsonIgnore] public ArtistApiModel? Artist { get; set; }

        [JsonIgnore] public IList<TrackApiModel>? Tracks { get; set; }

        public Album Convert() =>
            new()
            {
                Id = Id,
                ArtistId = ArtistId,
                Title = Title ?? string.Empty
            };

        public async Task<Album> ConvertAsync() =>
            new()
            {
                Id = Id,
                ArtistId = ArtistId,
                Title = Title ?? string.Empty
            };
    }
}