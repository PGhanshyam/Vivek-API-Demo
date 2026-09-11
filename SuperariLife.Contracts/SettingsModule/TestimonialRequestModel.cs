using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SuperariLife.Contracts.SettingsModule
{
    public class TestimonialRequestModel
    {
        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Author { get; set; } = string.Empty;
        public int DisplayOrder { get; set; } 
    }
}
