using System.Collections.Generic;

namespace WebLogic
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public List<Survey> Surveys { get; set; }

        public User()
        {
            Surveys = new List<Survey>();
        }
    }
}
