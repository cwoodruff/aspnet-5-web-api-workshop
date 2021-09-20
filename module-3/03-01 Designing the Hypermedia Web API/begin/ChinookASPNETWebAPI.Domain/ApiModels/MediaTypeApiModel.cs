using System.Collections.Generic;
using ChinookASPNETWebAPI.Domain.Converters;
using ChinookASPNETWebAPI.Domain.Entities;

namespace ChinookASPNETWebAPI.Domain.ApiModels
{
    public class MediaTypeApiModel : IConvertModel<MediaTypeApiModel, MediaType>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<TrackApiModel> Tracks { get; set; }

        public MediaType Convert() =>
            new()
            {
                Id = Id,
                Name = Name
            };
    }
}