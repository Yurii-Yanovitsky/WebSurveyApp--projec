using System;
using System.Collections.Generic;

namespace WebSurveyApp
{
    public class ReportBindingModel
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int SurveyId { get; set; }
        public SurveyBindingModel Survey { get; set; }
        public List<ResponseBindingModel> Responses { get; set; }
    }
}
