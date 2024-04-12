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
                    var principal = new ClaimsPrincipal(userIdentity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

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
                    ModelState.AddModelError(string.Empty, "Credenciais inválidas");
                }
            }

            return View(model);
        }

    }
}
