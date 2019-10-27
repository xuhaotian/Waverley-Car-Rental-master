using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SendGrid;

namespace Rental.ViewModels
{
    public class BulkEmailViewModel : DbContext
    {
        [AllowHtml]
        [Required(ErrorMessage = "Please enter a subject.")]
        public string Subject { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Please enter the contents")]
        public string Contents { get; set; }


    }
}