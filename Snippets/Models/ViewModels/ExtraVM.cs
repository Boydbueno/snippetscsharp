using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Snippets.Areas.Admin.Models.ViewModels;

namespace Snippets.Models.ViewModels
{
    public class ExtraVM
    {
        public List<SelectedTagData> Tags { get; set; }
        public List<Visibility> Visibilies { get; set; }
    }
}