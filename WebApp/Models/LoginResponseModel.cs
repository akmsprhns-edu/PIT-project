﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class LoginResponseModel : BaseResponseModel
    {
        public string Token { get; set; }
        public string Email { get; set; }
    }
}
