using System;
using System.Collections.Generic;
using System.Text;

namespace SuperariLife.Application.Models
{
    public class LoginUserData
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
