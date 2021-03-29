using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WebLogic.Services;

namespace WebSurveyApp.Controllers
{
    public class AccountController : Controller
    {

        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [AnonymousOnly("/Home/List")]
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [AnonymousOnly("/Home/List")]
        public async Task<IActionResult> Login(LoginBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var loginModel = model.ToServiceModel();

                var loginResult = await _accountService.LoginSuccessAsync(loginModel);

                if (loginResult.IsSuccess)
                {
                    var userBindingModel = loginResult.User.ToViewModel();
                    await SignInAsync(userBindingModel);

                    return RedirectToAction("List", "Home");
                }
            }

            ModelState.AddModelError("", "Inccorect login or password");

            return View(model);
        }

        [HttpGet]
        [AnonymousOnly("/Home/List")]
        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        [AnonymousOnly("/Home/List")]
        public async Task<IActionResult> Register(RegisterBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var registerModel = model.ToServiceModel();
                var registResult = await _accountService.RegistSuccessAsync(registerModel);

                if (registResult.IsSuccess)
                {

                    return RedirectToAction("Success");
                }
                else
                {
                    ModelState.AddModelError(nameof(model.Email), registResult.Error);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Success()
        {
            ViewBag.Username = User.Identity.Name;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login");
        }

        private async Task SignInAsync(UserBindingModel user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, "User")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme,
                ClaimTypes.Name, ClaimTypes.Role);

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(claimsPrincipal);
        }

    }
}
