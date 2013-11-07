using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Snippets.Models
{
    public class Snippet
    {

        [Key]
        public virtual int ID { get; set; }

        [Required]
        public virtual string Title { get; set; }

        public string Description { get; set; }

        [DataType(DataType.MultilineText)] 
        public virtual string Body { get; set; }

        public virtual List<Category> Categories { get; set; }
        public virtual List<Tag> Tags { get; set; }

        public virtual int VisibilityId { get; set; }
        public virtual Visibility Visibility { get; set; }
    }
}