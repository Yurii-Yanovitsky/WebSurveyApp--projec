using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebLogic.Services
{
    public class SurveyService
    {
        private readonly SurveyDbContext _context;

        public SurveyService(SurveyDbContext context)
        {
            _context = context;
        }
        public async Task CreateSurveyAsync(Survey surveyModel, string email)
        {
            var userModel = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            userModel.Surveys.Add(surveyModel);

            await _context.SaveChangesAsync();
        }

        public async Task EditSurveyAsync(Survey surveyModel)
        {
            _context.Update(surveyModel);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteSurveyAsync(int surveyId)
        {
            var surveyModel = await _context.Surveys
                 .Include("Reports")
                 .Include("Reports.Responses")
                 .FirstOrDefaultAsync(r => r.Id == surveyId);

            if (surveyModel != null)
            {
                var responses = surveyModel.Reports.SelectMany(x => x.Responses);

                if (responses != null)
                {
                    _context.Responses.RemoveRange(responses);
                }

                _context.Remove(surveyModel);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Survey>> GetSurveysAsync(string email)
        {
            var surveys = await _context.Surveys
                .Include(s => s.Reports)
                .Where(s => s.User.Email == email)
                .ToListAsync();

            return surveys;
        }

        public async Task<Survey> GetSurveyById(int surveyId)
        {
            var survey = await _context.Surveys
                .Include(s => s.Questions)
                .ThenInclude(q => q.Options)
                .FirstOrDefaultAsync(s => s.Id == surveyId);

            return survey;
        }
    }
}
