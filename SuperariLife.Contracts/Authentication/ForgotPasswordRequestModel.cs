using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SuperariLife.Contracts.Authentication
{
    public class ForgotPasswordRequestModel
    {
        private string _email = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [MaxLength(255)]
        public string Email
        {
            get => _email;
            set => _email = value?.Trim() ?? string.Empty;
        }
    }
}
