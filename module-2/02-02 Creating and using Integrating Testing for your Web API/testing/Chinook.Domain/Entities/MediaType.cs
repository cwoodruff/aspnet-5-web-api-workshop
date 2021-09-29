using Chinook.Domain.Converters;
using Chinook.Domain.ApiModels;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chinook.Domain.Entities
{
    public class MediaType : IConvertModel<MediaType, MediaTypeApiModel>
    {
        public MediaType()
        {
            Tracks = new HashSet<Track>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }


        public virtual ICollection<Track> Tracks { get; set; }

        public MediaTypeApiModel Convert() =>
            new()
            {
                Id = Id,
                Name = Name
            };

        public async Task<MediaTypeApiModel> ConvertAsync() =>
            new()
            {
                Id = Id,
                Name = Name
            };
    }
}