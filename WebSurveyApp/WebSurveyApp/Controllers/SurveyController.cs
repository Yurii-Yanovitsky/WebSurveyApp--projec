using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebLogic.Services;

namespace WebSurveyApp.Controllers
{
    [Authorize]
    public class SurveyController : Controller
    {
        private readonly SurveyService _surveyService;

        public SurveyController(SurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SurveyBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var surveyModel = model.ToServiceModel();
                string email = User.Identity.Name;

                await _surveyService.CreateSurveyAsync(surveyModel, email);

                return RedirectToAction($"Create", "Question", new { surveyId = surveyModel.Id });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromQuery] int surveyId)
        {
            var surveyModel = await _surveyService.GetSurveyById(surveyId);

            if (surveyModel != null)
            {
                var viewModel = surveyModel.ToViewModel();

                return View(viewModel);
            }

            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Title(SurveyBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var surveyModel = model.ToServiceModel();

                await _surveyService.EditSurveyAsync(surveyModel);

                return RedirectToAction($"Edit", "Survey", new { SurveyId = surveyModel.Id });
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int surveyId)
        {
            await _surveyService.DeleteSurveyAsync(surveyId);

            return RedirectToAction("List", "Home");
        }

        public async Task<IActionResult> Title([FromQuery] int surveyId)
        {
            var surveyModel = await _surveyService.GetSurveyById(surveyId);

            if (surveyModel != null)
            {
                var viewModel = surveyModel.ToViewModel();

                return View(viewModel);
            }

            return BadRequest();
        }
    }
}
