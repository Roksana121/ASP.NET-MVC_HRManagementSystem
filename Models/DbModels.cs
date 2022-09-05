using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Work_01.Models
{
    public class Department

    {

        public Department()
        {
            this.Employee = new List<Employee>();
        }

        [Display(Name = "Department Id")]
        public int DepartmentId { get; set; }

        [Required, StringLength(40), Display(Name = "Department Name")]
        public string DepartmentName { get; set; }

        public virtual ICollection<Employee> Employee{ get; set; }
    }
    public class Employee
    {
       
        public int EmployeeId { get; set; }

        [Required, StringLength(40), Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Please selected a gender!!!")]
        public string Gender { get; set; }
        [Required, Column(TypeName = "date"),Display(Name = "Join Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime JoinDate { get; set; }
        public string Designation { get; set; }
        [Required]
        public string Status { get; set; }

        [ForeignKey("Department")]
     
        public int DepartmentId { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = "The Email is required!!!")]
        [DataType(DataType.EmailAddress), EmailAddress]
        [Display(Name = "E-mail Address")]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }

        [Required,  Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        [Display(Name = "CurrentSalary")]
        public decimal SalaryAmmount { get; set; }
        [Display(Name = "Photo")]
        public string Image { get; set; }

        public virtual EmployeeAddress EmployeeAddress { get; set; }

        public virtual EmployeePhoto EmployeePhoto { get; set; }

        public virtual EmployeeSalary EmployeeSalary { get; set; }
        public virtual Department Department { get; set; }

        

    }
    public class EmployeeAddress
    {
        [Key, ForeignKey("Employee")]
      
        public int EmployeeId { get; set; }
        public string Address { get; set; }

        [Required(ErrorMessage = "email is required!!!")]
        [DataType(DataType.EmailAddress), EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }

        public virtual Employee Employee { get; set; }
    }
    public class EmployeePhoto
    {
        [Key, ForeignKey("Employee")]
       
        public int EmployeeId { get; set; }


        public string Image { get; set; }

        public virtual Employee Employee { get; set; }
    }

    public class EmployeeSalary
    {
        [Key, ForeignKey("Employee")]
       
        public int EmployeeId { get; set; }

        public decimal SalaryId { get; set; }

        [Required, Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal SalaryAmmount { get; set; }
        public virtual Employee Employee { get; set; }

    }

    public class Administrator
    {
        [Display(Name = "Administrator Id")]
        public int AdministratorId { get; set; }

        [Required, StringLength(40), Display(Name = "Administrator Name")]
        public string AdministratorName { get; set; }

        public virtual AdministratorAddress AdministratorAddress { get; set; }

        public virtual AdministratorPhoto AdministratorPhoto { get; set; }
    }

    public class AdministratorAddress
    {
        [Key, ForeignKey("Administrator")]
        [Display(Name = "Administrator Id")]
        public int AdministratorId { get; set; }

        [Required, StringLength(150)]
        public string Address { get; set; }

        [Required, StringLength(4), Display(Name = "PostCode")]
        public string PostCode { get; set; }

        public virtual Administrator Administrator { get; set; }
    }

    public class AdministratorPhoto
    {
        [Key, ForeignKey("Administrator")]
        [Display(Name = "Administrator Id")]
        public int AdministratorId { get; set; }


        public string Image { get; set; }

        public virtual Administrator Administrator { get; set; }
    }


    public class HRMSystemDbContext: DbContext
    {
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeAddress> EmployeeAddress { get; set; }
        public DbSet<EmployeePhoto> EmployeePhoto { get; set; }

        public DbSet<EmployeeSalary> EmployeeSalary { get; set; }
        public DbSet<Department> Department { get; set; }

        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<AdministratorAddress> AdministratorAddresses { get; set; }
        public DbSet<AdministratorPhoto> AdministratorPhoto { get; set; }
    }
}