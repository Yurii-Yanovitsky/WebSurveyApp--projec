using System.Collections.Generic;

namespace WebLogic
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
        public List<Option> Options { get; set; }
        public List<Response> Responses { get; set; }

        public Question()
        {
            Options = new List<Option>();
            Responses = new List<Response>();
        }
    }
}
