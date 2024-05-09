using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public  class CustomResponseAPI
    {
        public object Data { get; set; } = new { };

        public int StatusCode { get; set; } = StatusCodes.Status200OK;

        public string Message { get; set; } = string.Empty;
    }
}


