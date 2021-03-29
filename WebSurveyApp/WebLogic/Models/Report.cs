using System;
using System.Collections.Generic;

namespace WebLogic
{
    public class Report
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
        public List<Response> Responses { get; set; }

        public Report()
        {
            Responses = new List<Response>();
        }
    }
}
