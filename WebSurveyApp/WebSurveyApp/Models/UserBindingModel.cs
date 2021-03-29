using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebSurveyApp
{
    public class UserBindingModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required!")]
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public List<SurveyBindingModel> Surveys { get; set; }
    }
}
