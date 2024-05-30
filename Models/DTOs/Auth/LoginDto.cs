using System.ComponentModel.DataAnnotations;

namespace ChuckItApi.Models.DTOs.Auth
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
