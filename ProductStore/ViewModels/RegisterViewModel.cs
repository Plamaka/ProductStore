using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProductStore.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is requuired")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is requuired")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password dont match")]
        public string ConfirmPassword { get; set; }
    }
}
