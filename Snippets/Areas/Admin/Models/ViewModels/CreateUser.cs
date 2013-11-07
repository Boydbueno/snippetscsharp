using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Snippets.Models;

namespace Snippets.Areas.Admin.Models.ViewModels
{
    public class CreateUser
    {

        public RegisterModel registerModel { get; set; }
        public string[] roles { get; set; }

    }
}