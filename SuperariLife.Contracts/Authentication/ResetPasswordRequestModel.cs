using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SuperariLife.Contracts.Authentication
{
    public class ResetPasswordRequestModel
    {
        private string _resetToken = string.Empty;
        public string ResetToken
        {
            get => _resetToken;
            set => _resetToken = value;
        }
        public string? ResetPasswordToken
        {
            get => _resetToken;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _resetToken = value;
            }
        }

        [Required(ErrorMessage = "New password is required.")]
        [MinLength(6, ErrorMessage = "Password must contain at least 6 characters.")]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm password is required.")]
        [Compare(nameof(NewPassword), ErrorMessage = "New password and confirm password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
