﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Snippets.Models
{
    public class Snippet
    {

        [Key]
        public virtual int ID { get; set; }

        [Required]
        public virtual string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [DisplayName("Snippet")]
        public virtual string Body { get; set; }

        public virtual List<Tag> Tags { get; set; }

        public virtual int VisibilityId { get; set; }
        public virtual Visibility Visibility { get; set; }

        // I know, not usual for a snippet to have a zipcode, but this is a 'hook' to apply some 'complex' validation xD
        [RegularExpression("\\d{4} ?[a-zA-Z]{2}", ErrorMessage="This is no valid Dutch Zipcode")]
        public string ZipCode { get; set; }
    }
}