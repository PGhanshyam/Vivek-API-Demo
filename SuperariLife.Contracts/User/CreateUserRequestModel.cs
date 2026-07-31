using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SuperariLife.Contracts.User
{
    public class CreateUserRequestModel
    {
        [Required(ErrorMessage = "Role is required.")]
        [Range(1, long.MaxValue, ErrorMessage = "Please select a valid role.")]
        public long RoleId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(255)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(255)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must contain at least 6 characters.")]
        public string PasswordHash { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? PhoneNo { get; set; }

        public string? ProfileImage { get; set; }

        public string? Address { get; set; }

        [MaxLength(100)]
        public string? Country { get; set; }

        [MaxLength(100)]
        public string? State { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(20)]
        public string? ZipCode { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
