using System;
using System.Collections.Generic;
using System.Text;

namespace SuperariLife.Contracts.Authentication
{
    public class LoginResponseModel
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }
            = string.Empty;
        public long UserId { get; set; }

        public long RoleId { get; set; }

        public string RoleName { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

        public DateTime TokenExpiry { get; set; }
    }
}
