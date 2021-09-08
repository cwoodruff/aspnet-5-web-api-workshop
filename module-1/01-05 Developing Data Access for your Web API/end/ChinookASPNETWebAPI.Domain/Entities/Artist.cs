using System.Collections.Generic;

#nullable disable

namespace ChinookASPNETWebAPI.Domain.Entities
{
    public partial class Artist
    {
        public Artist()
        {
            Albums = new HashSet<Album>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
