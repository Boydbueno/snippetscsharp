﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Snippets.Areas.Admin.Models
{
    public class Role
    {
        [Required]
        public string Name { get; set; }

        public Role()
        {
        }

        public Role(string name)
        {
            this.Name = name;
        }
    }
}