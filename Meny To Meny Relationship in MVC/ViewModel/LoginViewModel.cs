using System.ComponentModel.DataAnnotations;

namespace Meny_To_Meny_Relationship_in_MVC.ViewModel
{
    public class LoginViewModel
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage ="Email address is requuired")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
