using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IterationWebApp.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "Email address is required!")]
        [EmailAddress]
        [Display(Name ="E-Mail")]
        public string Email { get; set; }

        [DataType(DataType.Password,ErrorMessage ="Password Must have Upper Letters and numbers!")]
        [Required(ErrorMessage ="Password must required!")]
        [MinLength(8,ErrorMessage ="Required Minimum 8 Characters long!")]
        public string Password { get; set; }

        [Required(ErrorMessage ="Password Confirmation is required")]
        [Compare(nameof(Password), ErrorMessage ="Password must match")]
        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
