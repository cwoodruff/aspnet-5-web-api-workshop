using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Chinook.Domain.Converters;
using Chinook.Domain.Entities;

namespace Chinook.Domain.ApiModels
{
    public class MediaTypeApiModel : IConvertModel<MediaTypeApiModel, MediaType>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore] public IList<TrackApiModel> Tracks { get; set; }

        public MediaType Convert() =>
            new()
            {
                Id = Id,
                Name = Name
            };

        public async Task<MediaType> ConvertAsync() =>
            new()
            {
                Id = Id,
                Name = Name
            };
    }
}