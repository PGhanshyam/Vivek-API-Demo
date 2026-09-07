using System;
using System.Collections.Generic;
using System.Text;

namespace SuperariLife.Contracts.Settings
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; } = string.Empty;
        public int SmtpPort { get; set; }
        public string SmtpUserName { get; set; } = string.Empty;
        public string SmtpPassword { get; set; } = string.Empty;
        public string SenderName { get; set; } = string.Empty;
        public string SenderEmail { get; set; } = string.Empty;
        public bool EnableSsl { get; set; } 
    }
}
