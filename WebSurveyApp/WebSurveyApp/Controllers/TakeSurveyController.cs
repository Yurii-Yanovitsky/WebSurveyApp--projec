using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebLogic.Services;

namespace WebSurveyApp.Controllers
{
    public class TakeSurveyController : Controller
    {
        private readonly ReportService _reportService;
        private readonly SurveyService _surveyService;

        public TakeSurveyController(ReportService reportService, SurveyService surveyService)
        {
            _reportService = reportService;
            _surveyService = surveyService;
        }

        public async Task<IActionResult> Start([FromRoute] int surveyId)
        {
            var surveyModel = await _surveyService.GetSurveyById(surveyId);

            if (surveyModel != null)
            {
                var viewModel = surveyModel.ToViewModel();

                return View(viewModel);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Finish(ReportBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var reportModel = model.ToServiceModel();
                await _reportService.AddReportAsync(reportModel);

                return View();
            }

            ModelState.AddModelError("", "It seems like you haven't answerd all the questions");

            return NotFound();
        }
    }
}