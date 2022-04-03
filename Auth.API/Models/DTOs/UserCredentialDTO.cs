using System.ComponentModel.DataAnnotations;

namespace Auth.API.Models.DTOs
{
    public class UserCredentialDTO
    {
        [MaxLength(20)]
        public string Username { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        public string Token { get; set; }
    }
}
