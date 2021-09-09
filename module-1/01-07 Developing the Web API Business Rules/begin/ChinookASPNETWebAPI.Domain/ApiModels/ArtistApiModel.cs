using System.Collections.Generic;
using System.Threading.Tasks;
using ChinookASPNETWebAPI.Domain.Converters;
using ChinookASPNETWebAPI.Domain.Entities;

namespace ChinookASPNETWebAPI.Domain.ApiModels
{
    public class ArtistApiModel : IConvertModel<ArtistApiModel, Artist>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<AlbumApiModel> Albums { get; set; }

        public Artist Convert() =>
            new()
            {
                Id = Id,
                Name = Name ?? string.Empty
            };
    }
}