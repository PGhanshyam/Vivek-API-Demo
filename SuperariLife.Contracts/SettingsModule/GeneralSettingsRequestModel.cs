using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SuperariLife.Contracts.SettingsModule
{
    public class GeneralSettingsRequestModel
    {
        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string AdminEmail { get; set; } = string.Empty;

        [Range(1, 100)]
        public decimal TaxPercentage { get; set; }

        [MaxLength(500)]
        public string? FacebookUrl { get; set; }
        
        [MaxLength(500)]
        public string? TwitterUrl { get; set; }
        
        [MaxLength(500)]
        public string? InstagramUrl { get; set; }
        
        [MaxLength(500)]
        public string? YoutubeUrl { get; set; }
    }
}
