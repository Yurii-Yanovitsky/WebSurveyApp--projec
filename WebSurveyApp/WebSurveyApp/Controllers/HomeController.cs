using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebLogic.Services;

namespace WebSurveyApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly SurveyService _surveyService;

        public HomeController(SurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        [Authorize]
        public async Task<IActionResult> List()
        {
            string email = User.Identity.Name;
            var surveys = await _surveyService.GetSurveysAsync(email);

            var viewModel = surveys.Select(s => s.ToViewModel()).ToList();

            return View(viewModel);
        }
    }
}
