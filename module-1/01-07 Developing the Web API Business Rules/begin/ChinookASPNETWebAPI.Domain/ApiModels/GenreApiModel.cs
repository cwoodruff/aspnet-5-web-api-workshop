using System.Collections.Generic;
using System.Threading.Tasks;
using ChinookASPNETWebAPI.Domain.Converters;
using ChinookASPNETWebAPI.Domain.Entities;

namespace ChinookASPNETWebAPI.Domain.ApiModels
{
    public class GenreApiModel : IConvertModel<GenreApiModel, Genre>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<TrackApiModel> Tracks { get; set; }

        public Genre Convert() =>
            new()
            {
                Id = Id,
                Name = Name
            };
    }
}