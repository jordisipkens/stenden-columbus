﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColombusWebapplicatie.Models
{
    public class LoginResponse
    {
        public User User { get; set; }
        public string Token { get; set; }
    }
}