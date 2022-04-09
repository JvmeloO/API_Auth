using System;
using System.Collections.Generic;

namespace Auth.Domain.Entities
{
    public partial class User
    {
        public User()
        {
            Roles = new HashSet<Role>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<Role> Roles { get; set; }
    }
}
