using Chinook.Domain.Converters;
using Chinook.Domain.ApiModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chinook.Domain.Entities
{
    public class Genre : IConvertModel<Genre, GenreApiModel>
    {
        public Genre()
        {
            Tracks = new HashSet<Track>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<Track> Tracks { get; set; }

        public GenreApiModel Convert() =>
            new()
            {
                Id = Id,
                Name = Name
            };

        public async Task<GenreApiModel> ConvertAsync() =>
            new()
            {
                Id = Id,
                Name = Name
            };
    }
}