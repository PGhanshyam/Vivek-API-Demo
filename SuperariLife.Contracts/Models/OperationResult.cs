using System;
using System.Collections.Generic;
using System.Text;

namespace SuperariLife.Application.Models
{
    public class OperationResult
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; } = string.Empty;

        public static implicit operator bool(OperationResult v)
        {
            throw new NotImplementedException();
        }
    }
}
