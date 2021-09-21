using System.ComponentModel.DataAnnotations;

namespace ChinookASPNETWebAPI.Domain.Identity
{
    public class RegisterModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
