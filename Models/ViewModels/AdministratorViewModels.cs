using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Work_01.Models.ViewModels
{
    public class AdministratorViewModels
    {
        [Display(Name = "Administrator Id")]
        public int AdministratorId { get; set; }

        [Required, StringLength(40), Display(Name = "Administrator Name")]
        public string AdministratorName { get; set; }
     
        public string Address { get; set; }

        [Required, StringLength(4), Display(Name = "PostCode")]
        public string PostCode { get; set; }
        public HttpPostedFileBase Image { get; set; }
    }
}