using System.Threading.Tasks;

namespace WebLogic.Services
{
    public class OptionService
    {
        private readonly SurveyDbContext _context;

        public OptionService(SurveyDbContext context)
        {
            _context = context;
        }

        public async Task CreateOptionAsync(Option optionModel)
        {
            await _context.Options.AddAsync(optionModel);
            await _context.SaveChangesAsync();
        }

        public async Task EditOptionAsync(Option optionModel)
        {
            _context.Options.Update(optionModel);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteOptionByIdAsync(int optionId)
        {
            var optionModel = await GetOptionByIdAsync(optionId);

            _context.Options.Remove(optionModel);
            await _context.SaveChangesAsync();
        }

        public async Task<Option> GetOptionByIdAsync(int optionId)
        {
            var optionModel = await _context.Options.FindAsync(optionId);

            return optionModel;
        }
    }
}
