using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebLogic.Services;
using WebSurveyApp.Models;

namespace WebSurveyApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            string email = User.Identity.Name;
            var userModel = await _userService.GetUserByEmailAsync(email);

            if (userModel != null)
            {
                var viewModel = userModel.ToViewModel();

                return View(viewModel);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> SaveProfile(UserBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var userModel = model.ToServiceModel();
                await _userService.ChangeUserNameAsync(userModel);

                return View("Profile", model);
            }

            return View("Profile", model);
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            string email = User.Identity.Name;
            var userModel = await _userService.GetUserByEmailAsync(email);

            if (userModel != null)
            {
                var viewModel = userModel.ToChangePasswordViewModel();

                return View(viewModel);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var changeResult = await _userService.ChangePasswordAsync(model.UserId, model.NewPassword, model.OldPassword);

                if (changeResult.IsSuccess)
                {

                    return View("Success");
                }

                ModelState.AddModelError("", changeResult.Error);
            }

            return View(model);
        }
    }
}