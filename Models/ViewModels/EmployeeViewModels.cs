using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Work_01.Models.ViewModels
{
    public class EmployeeViewModels
    {
       
            public int EmployeeId { get; set; }

            [Required, StringLength(40), Display(Name = "Employee Name")]
            public string EmployeeName { get; set; }

            [Required(ErrorMessage = "Please selected a gender!!!")]

            public string Gender { get; set; }

            [Required, Column(TypeName = "date"), Display(Name = "Join Date")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public DateTime JoinDate { get; set; }

            [Required, Display(Name = "Designation")]
            public string Designation { get; set; }

            [Required]
            public string Status { get; set; }

            [ForeignKey("Department")]
            public int DepartmentId { get; set; }
            public string Address { get; set; }

            [Required(ErrorMessage = "The Email is required!!!")]
            [DataType(DataType.EmailAddress), EmailAddress]
            [Display(Name = "E-mail ")]
            public string Email { get; set; }

            [Required]
            public string Phone { get; set; }

            [Required, Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
            [Display(Name = "CurrentSalary")]
            public decimal SalaryAmmount { get; set; }
            [Display(Name = "Photo")]
            public string Image { get; set; }
            public HttpPostedFileBase Picture { get; set; }
        
    }
}