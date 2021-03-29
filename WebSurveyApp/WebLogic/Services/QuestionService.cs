using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebLogic.Services
{
    public class QuestionService
    {
        private readonly SurveyDbContext _context;

        public QuestionService(SurveyDbContext context)
        {
            _context = context;
        }

        public async Task CreateQuestionAsync(Question questionModel)
        {
            await _context.AddAsync(questionModel);
            await _context.SaveChangesAsync();
        }

        public async Task<Question> EditQuestionAsync(int questionId)
        {
            var questionModel = await _context.Questions.FindAsync(questionId);

            return questionModel;
        }

        public async Task EditQuestionAsync(Question questionModel)
        {
            _context.Questions.Update(questionModel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteQuestionAsync(int questionId)
        {
            var questionModel = await _context.Questions.FindAsync(questionId);

            if (questionModel != null)
            {
                var responses = await _context.Responses
                    .Where(r => r.QuestionId == questionModel.Id)
                    .ToListAsync();

                if (responses != null)
                {
                    _context.Responses.RemoveRange(responses);
                }

                _context.Questions.Remove(questionModel);

                await _context.SaveChangesAsync();
            }
        }

    }
}
