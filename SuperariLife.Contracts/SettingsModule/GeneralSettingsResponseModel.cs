using System;
using System.Collections.Generic;
using System.Text;

namespace SuperariLife.Contracts.SettingsModule
{
    public class GeneralSettingsResponseModel
    {
        public long GeneralSettingsId { get; set; }
        public string AdminEmail { get; set; } = string.Empty;
        public decimal TaxPercentage { get; set; }
        public string? FacebookUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public string? YoutubeUrl { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
