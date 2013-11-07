using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Snippets.Models;

namespace Snippets.Areas.Admin.Models.ViewModels
{
    public class UserRoles
    {
        public UserProfile userProfile { get; set; }
        public string[] roles { get; set; }
    }
}