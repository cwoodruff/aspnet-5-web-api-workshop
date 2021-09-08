using System.Collections.Generic;

#nullable disable

namespace ChinookASPNETWebAPI.Domain.Entities
{
    public partial class MediaType
    {
        public MediaType()
        {
            Tracks = new HashSet<Track>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Track> Tracks { get; set; }
    }
}
