using System;
using System.Collections.Generic;
using System.Text;

namespace SuperariLife.Contracts.Authentication
{
    public class LoginUserResponseModel
    {
        public long UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public bool MustChangePassword { get; set; }
    }
}
