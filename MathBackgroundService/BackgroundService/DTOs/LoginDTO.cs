using System.ComponentModel.DataAnnotations;

namespace BackgroundServiceVote.DTOs
{
    public class LoginDTO
    {
        [Required]
        public String Username { get; set; } = null!;
        [Required]
        public String Password { get; set; } = null!;
    }
}
