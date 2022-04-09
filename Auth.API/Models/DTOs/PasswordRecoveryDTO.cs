using System.ComponentModel.DataAnnotations;

namespace Auth.API.Models.DTOs
{
    public class PasswordRecoveryDTO
    {
        [MaxLength(50)]
        public string Email { get; set; } = null!;
    }
}
