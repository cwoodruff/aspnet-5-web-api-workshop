using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Chinook.Domain.Converters;
using Chinook.Domain.Entities;

namespace Chinook.Domain.ApiModels
{
    public class GenreApiModel : IConvertModel<GenreApiModel, Genre>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore] public IList<TrackApiModel> Tracks { get; set; }

        public Genre Convert() =>
            new()
            {
                Id = Id,
                Name = Name
            };

        public async Task<Genre> ConvertAsync() =>
            new()
            {
                Id = Id,
                Name = Name
            };
    }
}