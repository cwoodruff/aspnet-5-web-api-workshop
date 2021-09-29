using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Chinook.Domain.Converters;
using Chinook.Domain.Entities;

namespace Chinook.Domain.ApiModels
{
    public sealed class TrackApiModel : IConvertModel<TrackApiModel, Track>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? AlbumId { get; set; }
        public string? AlbumName { get; set; }
        public int MediaTypeId { get; set; }
        public string? MediaTypeName { get; set; }
        public int? GenreId { get; set; }
        public string? GenreName { get; set; }
        public string? Composer { get; set; }
        public int Milliseconds { get; set; }
        public int? Bytes { get; set; }
        public decimal UnitPrice { get; set; }

        [JsonIgnore] public IList<InvoiceLineApiModel>? InvoiceLines { get; set; }

        [JsonIgnore] public IList<PlaylistTrackApiModel>? PlaylistTracks { get; set; }

        [JsonIgnore] public AlbumApiModel? Album { get; set; }
        public GenreApiModel? Genre { get; set; }
        public MediaTypeApiModel? MediaType { get; set; }

        public Track Convert() =>
            new()
            {
                Id = Id,
                Name = Name,
                AlbumId = AlbumId,
                MediaTypeId = MediaTypeId,
                GenreId = GenreId,
                Composer = Composer,
                Milliseconds = Milliseconds,
                Bytes = Bytes,
                UnitPrice = UnitPrice
            };

        public async Task<Track> ConvertAsync() =>
            new()
            {
                Id = Id,
                Name = Name,
                AlbumId = AlbumId,
                MediaTypeId = MediaTypeId,
                GenreId = GenreId,
                Composer = Composer,
                Milliseconds = Milliseconds,
                Bytes = Bytes,
                UnitPrice = UnitPrice
            };
    }
}