#nullable disable

namespace ChinookASPNETWebAPI.Domain.Entities
{
    public partial class AlbumWithArtistName
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ArtistId { get; set; }
        public string Name { get; set; }
    }
}
