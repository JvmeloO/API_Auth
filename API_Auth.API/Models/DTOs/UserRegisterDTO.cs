using System.ComponentModel.DataAnnotations;

namespace API_Auth.API.Models.DTOs
{
    public class UserRegisterDTO
    {
        [MaxLength(20)]
        public string Username { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(30)]
        public string Password { get; set; }
    }
}
