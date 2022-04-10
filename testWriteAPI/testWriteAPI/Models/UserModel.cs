﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testWriteAPI.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool Management { get; set; }
        public bool IsDeleted { get; set; }
    }
}