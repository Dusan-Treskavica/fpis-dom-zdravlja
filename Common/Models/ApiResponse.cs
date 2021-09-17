using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class ApiResponse
    {
        public int HttpStatusCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
