﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HeThongQuanLy.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Xin nhap username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Xin nhap pass")]
        public string Password { get; set; }
    }
}