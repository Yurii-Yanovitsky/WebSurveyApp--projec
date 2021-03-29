using System.ComponentModel.DataAnnotations;

namespace WebSurveyApp
{
    public class RegisterBindingModel
    {
        [Required(ErrorMessage = "Name isn't specified")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Login isn't specified")]
        [EmailAddress]
        [Display(Name = "Login")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password isn't specified")]
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Password length cannot be less then 6 characters")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        //[StringLength(30, MinimumLength = 5)]
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Password length cannot be less then 6 characters")]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
