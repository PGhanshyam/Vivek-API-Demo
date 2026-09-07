using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SuperariLife.Contracts.User
{
    public class UpdateUserRequestModel
    {
        [Required]
        [Range(1, long.MaxValue)]
        public long RoleId { get; set; }

        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? PhoneNo { get; set; }
        public IFormFile? ProfileImage { get; set; }
        public string? ProfileImagePath { get; set; }
        public string? Address { get; set; }

        [MaxLength(100)]
        public string? Country { get; set; }

        [MaxLength(100)]
        public string? State { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(20)]
        public string? ZipCode { get; set; }
        public bool IsActive { get; set; }
    }
}
