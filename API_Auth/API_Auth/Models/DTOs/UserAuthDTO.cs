using System.ComponentModel.DataAnnotations;

namespace API_Auth.Models.DTOs
{
    public class UserAuthDTO
    {
        [MaxLength(20)]
        public string Username { get; set; }

        [MaxLength(30)]
        public string Password { get; set; }
    }
}
