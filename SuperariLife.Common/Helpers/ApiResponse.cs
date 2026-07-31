using System;
using System.Collections.Generic;
using System.Text;

namespace SuperariLife.Common.Helpers
{
    // Non-generic base response preserved for callers that expect ApiResponse (no generic)
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new();
    }

    // Generic ApiResponse<T> to allow returning typed Data payloads (e.g. ApiResponse<object>)
    public class ApiResponse<T> : ApiResponse
    {
        public T? Data { get; set; }
    }
}
