using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snippets.Areas.Admin.Models.ViewModels
{
    public class RoleUsers
    {

        public Role Role { get; set; }
        public string[] Users { get; set; }

    }
}