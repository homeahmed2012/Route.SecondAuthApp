using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace SecondAuthApp.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter salary")]
        [Display(Name = "Salary")]
        public double Salary { get; set; }

        [Required(ErrorMessage = "Please enter salary")]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        public virtual IdentityUser AppUser { get; set; }
        public string AppUserId { get; set; }
    }
}
