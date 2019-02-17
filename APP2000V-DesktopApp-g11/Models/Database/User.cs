﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP2000V_DesktopApp_g11.Models.Database
{
    class User
    {
        [Key]
        public string Username { get; set; }
        public string Password { get; set; }
        public int UserType { get; set; }
    }
}