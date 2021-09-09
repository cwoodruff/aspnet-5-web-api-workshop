using System.Collections.Generic;
using System.Threading.Tasks;
using ChinookASPNETWebAPI.Domain.ApiModels;
using ChinookASPNETWebAPI.Domain.Converters;

namespace ChinookASPNETWebAPI.Domain.Entities
{
    public class Genre : IConvertModel<Genre, GenreApiModel>
    {
        public Genre()
        {
            Tracks = new HashSet<Track>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Track> Tracks { get; set; }

        public GenreApiModel Convert() =>
            new()
            {
                Id = Id,
                Name = Name
            };
    }
}