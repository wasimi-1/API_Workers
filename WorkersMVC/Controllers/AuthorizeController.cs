using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WorkersMVC.Model;
using WorkersMVC.Services;

namespace WorkersMVC.Controllers
{
    public class AuthorizeController : Controller
    {
        private readonly ApiUserService _apiService;

        public AuthorizeController()
        {
            _apiService = new ApiUserService();
        }
        public IActionResult Login()
        {
            UserLoginRequest obj = new UserLoginRequest();
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginRequest obj)
        {
            UserLoginResponse objResponse = new UserLoginResponse();
            objResponse = await _apiService.AuthenticateUser(obj);
            if (objResponse != null && objResponse.Token.ToString() != "")
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, objResponse.UserDetails.Name));
                identity.AddClaim(new Claim(ClaimTypes.Role, objResponse.UserDetails.Role));

                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                HttpContext.Session.SetString("ApiToken", objResponse.Token);
                return RedirectToAction("Index", "Home");
            } else
            {
                HttpContext.Session.SetString("ApiToken", "");
                ModelState.AddModelError("password", "Nieprawidłowa nazwa uzytkownika lub hasło.");
            }
            return View(objResponse);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString("ApiToken", "");
            return RedirectToAction("Login", "Authorize");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
