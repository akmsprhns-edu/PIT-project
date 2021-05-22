using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class BaseResponseModel
    {
        public bool Success { get; set; } = true;
        public string Error { get; set; }

    }
}
