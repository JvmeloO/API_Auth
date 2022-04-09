﻿using System.ComponentModel.DataAnnotations;

namespace Auth.API.Models.DTOs
{
    public class UserCredentialDTO
    {
        [MaxLength(20)]
        public string Username { get; set; } = null!;

        [MaxLength(50)]
        public string Email { get; set; } = null!;

        public string Token { get; set; } = null!;
    }
}
