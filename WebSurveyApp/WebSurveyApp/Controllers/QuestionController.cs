using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebLogic.Services;

namespace WebSurveyApp.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
        private readonly QuestionService _questionService;

        public QuestionController(QuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public IActionResult Create([FromQuery] int surveyId)
        {
            var viewModel = new QuestionBindingModel() { SurveyId = surveyId };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] QuestionBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var questionModel = model.ToServiceModel();
                await _questionService.CreateQuestionAsync(questionModel);

                int questionId = questionModel.Id;
                int surveyId = questionModel.SurveyId;

                return RedirectToAction($"Create", "Option", new { surveyId, questionId });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromQuery] int questionId)
        {
            var questionModel = await _questionService.EditQuestionAsync(questionId);

            if (questionModel != null)
            {
                var viewModel = questionModel.ToViewModel();

                return View(viewModel);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] QuestionBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var questionModel = model.ToServiceModel();
                await _questionService.EditQuestionAsync(questionModel);

                return RedirectToAction("Edit", "Survey", new { questionModel.SurveyId });
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int surveyId, [FromForm] int questionId)
        {
            await _questionService.DeleteQuestionAsync(questionId);

            return RedirectToAction($"Edit", "Survey", new { surveyId });
        }
    }
}
