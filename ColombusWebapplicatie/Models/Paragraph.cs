using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ColombusWebapplicatie.Models
{
    public class Paragraph
    {
        public string Text { get; set; }

        public string ImagePath { get; set; }

        public bool AlignImageLeft { get; set; }
    }
}