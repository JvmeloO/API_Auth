﻿using System.ComponentModel.DataAnnotations;

namespace Auth.API.Models.DTOs
{
    public class UserAuthDTO
    {
        [MaxLength(20)]
        public string Username { get; set; } = null!;

        [MaxLength(30)]
        public string Password { get; set; } = null!;
    }
}
