using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebLogic.Services
{
    public class ReportService
    {
        private readonly SurveyDbContext _context;

        public ReportService(SurveyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Report>> GetReportsAsync(int surveyId)
        {
            var reports = await _context.Reports
                .AsSplitQuery()
                .Include("Responses")
                .Include("Responses.Question")
                .Include("Responses.Option")
                .Where(qs => qs.SurveyId == surveyId)
                .ToListAsync();

            return reports;
        }

        public async Task AddReportAsync(Report reportModel)
        {
            await _context.Reports.AddAsync(reportModel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReportsAsync(Report reportModel)
        {
            Response[] responses = _context.Responses.Where(r => r.ReportId == reportModel.Id).ToArray();

            _context.Responses.RemoveRange(responses);
            _context.Reports.Remove(reportModel);

            await _context.SaveChangesAsync();
        }
    }
}
