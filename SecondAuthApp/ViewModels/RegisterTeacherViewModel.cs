using System;
using System.ComponentModel.DataAnnotations;

namespace SecondAuthApp.ViewModels
{
    public class RegisterTeacherViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        
        [Required(ErrorMessage = "Please enter salary")]
        [Display(Name = "Salary")]
        public double Salary { get; set; }
        
        [Required(ErrorMessage = "Please enter salary")]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }
    }
}
