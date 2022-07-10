using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataAccess.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int? YearOfBirth { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string JobTitle { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}
