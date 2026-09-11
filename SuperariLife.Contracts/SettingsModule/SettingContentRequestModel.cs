using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SuperariLife.Contracts.SettingsModule
{
    public class SettingContentRequestModel
    {
        [Required]
        public string Content { get; set; } = string.Empty;
    }
}
