using System;
using System.Collections.Generic;
using System.Text;

namespace SuperariLife.Contracts.SettingsModule
{
    public class SettingContentResponseModel
    {
        public long SettingContentId { get; set; }
        public string SettingType { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
