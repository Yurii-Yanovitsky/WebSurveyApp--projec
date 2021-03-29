using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebLogic.Services;

namespace WebSurveyApp.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly ReportService _reportService;

        public ReportController(ReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task<IActionResult> List([FromQuery] int surveyId)
        {
            var reports = await _reportService.GetReportsAsync(surveyId);

            if (reports != null)
            {
                var viewModel = reports.Select(r => r.ToViewModel()).ToList();

                return View(viewModel);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ReportBindingModel model)
        {
            if (model.Id != 0)
            {
                var reportModel = model.ToServiceModel();
                await _reportService.DeleteReportsAsync(reportModel);

                return RedirectToAction("List", new { model.SurveyId });
            }

            return BadRequest();
        }
    }
}