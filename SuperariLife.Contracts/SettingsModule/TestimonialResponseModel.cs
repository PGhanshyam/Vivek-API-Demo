using System;
using System.Collections.Generic;
using System.Text;

namespace SuperariLife.Contracts.SettingsModule
{
    public class TestimonialResponseModel
    {
        public long TestimonialId { get; set; }
        public string Content { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
