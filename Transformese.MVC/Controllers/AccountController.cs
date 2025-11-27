using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Transformese.MVC.Services;
using TransformeseMVC.Web.ViewModels;
using Transformese.Api.DTOs;


namespace TransformeSeMVC.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsuarioApiClient _api;

        public AccountController(IUsuarioApiClient api)
        {
            _api = api;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // ---- AUTENTICAÇÃO VIA API ----
            var (token, nome, tipo) = await _api.AuthenticateAsync(model.Email, model.Password);

            if (token is null)
            {
                ViewBag.Error = "Usuário ou senha inválidos.";
                return View(model);
            }

            // ---- CRIAR CLAIMS ----
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, nome),
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.Role, tipo)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // ---- LOGIN VIA COOKIE ----
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe,
                    ExpiresUtc = DateTime.UtcNow.AddHours(8)
                }
            );

            // ---- SALVAR TOKEN NA SESSÃO (IMPORTANTE!) ----
            HttpContext.Session.SetString("JwtToken", token);
            HttpContext.Session.SetString("Usuario", nome);
            HttpContext.Session.SetString("TipoUsuario", tipo);

            // ---- REDIRECIONAMENTO ----
            return tipo switch
            {
                "Administrador" => RedirectToAction("Index", "Dashboard"),
                _ => RedirectToAction("Index", "Candidato"),
            };
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
