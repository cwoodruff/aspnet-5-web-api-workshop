using System.Collections.Generic;
using System.Threading.Tasks;
using ChinookASPNETWebAPI.Domain.ApiModels;
using ChinookASPNETWebAPI.Domain.Converters;

namespace ChinookASPNETWebAPI.Domain.Entities
{
    public class MediaType : IConvertModel<MediaType, MediaTypeApiModel>
    {
        public MediaType()
        {
            Tracks = new HashSet<Track>();
        }

        public int Id { get; set; }
        public string Name { get; set; }


        public virtual ICollection<Track> Tracks { get; set; }

        public MediaTypeApiModel Convert() =>
            new()
            {
                Id = Id,
                Name = Name
            };
    }
}