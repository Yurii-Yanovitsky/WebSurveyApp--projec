using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebSurveyApp
{
    public class SurveyBindingModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title isn't specified")]
        public string Title { get; set; }
        public DateTime Modified { get; set; }
        public int UserId { get; set; }
        public UserBindingModel User { get; set; }
        public List<QuestionBindingModel> Questions { get; set; }
        public List<ReportBindingModel> Reports { get; set; }
        public string Date => Modified.ToShortDateString();
        public string Time => Modified.ToShortTimeString();
    }
}