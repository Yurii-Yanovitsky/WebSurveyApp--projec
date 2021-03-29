using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebLogic.Services;

namespace WebSurveyApp.Controllers
{
    [Authorize]
    public class OptionController : Controller
    {
        private readonly OptionService _optionService;

        public OptionController(OptionService optionService)
        {
            _optionService = optionService;
        }

        [HttpGet]

        public IActionResult Create([FromQuery] int surveyId, [FromQuery] int questionId)
        {

            var viewModel = new OptionBindingModel() { QuestionId = questionId };

            ViewBag.SurveyId = surveyId;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] int surveyId, [FromForm] OptionBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var optionModel = model.ToServiceModel();
                await _optionService.CreateOptionAsync(optionModel);

                return RedirectToAction($"Edit", "Survey", new { surveyId });
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit([FromQuery] int surveyId, [FromQuery] int optionId)
        {
            var optionModel = await _optionService.GetOptionByIdAsync(optionId);

            if (optionModel != null)
            {
                var viewModel = optionModel.ToViewModel();
                ViewBag.SurveyId = surveyId;

                return View(viewModel);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] int surveyId, [FromForm] OptionBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var optionModel = model.ToServiceModel();
                await _optionService.EditOptionAsync(optionModel);

                return RedirectToAction($"Edit", "Survey", new { surveyId });
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int surveyId, [FromForm] int optionId)
        {
            await _optionService.DeleteOptionByIdAsync(optionId);

            return RedirectToAction($"Edit", "Survey", new { surveyId });
        }
    }
}