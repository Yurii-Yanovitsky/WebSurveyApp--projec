using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebSurveyApp
{
    public class OptionBindingModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Text isn't specified")]
        public string Text { get; set; }
        public int QuestionId { get; set; }
        public QuestionBindingModel Question { get; set; }
        public List<ResponseBindingModel> Responses { get; set; }
    }
}
