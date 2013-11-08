using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Snippets.Models
{
    public class Visibility
    {

        public virtual int ID { get; set; }

        [Required]
        [DisplayName("Visibility")]
        public virtual string Label { get; set; }
        public virtual string Description { get; set; }

    }
}