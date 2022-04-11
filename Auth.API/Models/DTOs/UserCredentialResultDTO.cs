namespace Auth.API.Models.DTOs
{
    public class UserCredentialResultDTO
    {
        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Token { get; set; } = null!;
    }
}
