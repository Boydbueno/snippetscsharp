using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snippets.Areas.Admin.Models.ViewModels
{
    public class SelectedTagData
    {

        public int TagId { get; set; }
        public string Label { get; set; }
        public bool Selected { get; set; }

    }
}