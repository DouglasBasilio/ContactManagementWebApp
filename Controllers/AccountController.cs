using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Alfasoft.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                if (model.Login == "alfasoft" && model.Password == "pwd123")
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Login)
                    };

                    var userIdentity = new ClaimsIdentity(claims, "login");
                    
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync("CookieAuthentication", principal, new AuthenticationProperties());

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    TempData["erro"] = "Invalid credentials";
                    return Redirect(returnUrl);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Logoff()
        {
            await HttpContext.SignOutAsync("CookieAuthentication");
            return RedirectToAction("Index", "Home");
        }
    }
}
