using System;
using System.Collections.Generic;

namespace WebLogic
{
    public class Survey
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Modified { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Question> Questions { get; set; }
        public List<Report> Reports { get; set; }

        public Survey()
        {
            Questions = new List<Question>();
            Reports = new List<Report>();
        }
    }
}
