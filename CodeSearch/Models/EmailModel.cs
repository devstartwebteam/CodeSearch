using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeSearch.Models
{
    public class EmailModel
    {
        [Required, Display(Name = "Individual and/or Company Name")]
        public string FromName { get; set; }
        [Required, Display(Name = "ZIP Code")]
        public string FromZip { get; set; }
        [Required, Display(Name = "Your Email"), EmailAddress]
        public string FromEmail { get; set; }
        [Required, Display(Name = "Your Phone")]
        public string FromPhone { get; set; }

        [Required]
        public string Message { get; set; }
    }
}