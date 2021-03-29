using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebSurveyApp
{
    public class QuestionBindingModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Text isn't specified")]
        public string Text { get; set; }
        public int SurveyId { get; set; }
        public SurveyBindingModel Survey { get; set; }
        public List<OptionBindingModel> Options { get; set; }
        public List<ResponseBindingModel> Responses { get; set; }
    }
}
