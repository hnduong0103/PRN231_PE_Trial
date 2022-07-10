using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DataAccess.Models
{
    public class AccountDb
    {
        [Key]
        [Required(ErrorMessage = "Email is required!!")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Password is required!!")]
        public string AccountPassword { get; set; }
        public string AccountFullName { get; set; }
        public int? AccountRole { get; set; }
    }
}
