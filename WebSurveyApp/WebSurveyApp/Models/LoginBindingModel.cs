using System.ComponentModel.DataAnnotations;

namespace WebSurveyApp
{
    public class LoginBindingModel
    {
        [Required(ErrorMessage = "Login isn't specified")]
        [EmailAddress]
        [Display(Name = "Login")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password isn't specified")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
