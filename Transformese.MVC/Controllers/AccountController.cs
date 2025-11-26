using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Transformese.MVC.Services;
using Transformese.MVC.ViewModels;
using TransformeseMVC.Web.ViewModels;

namespace Transformese.MVC.Controllers
{
    // A herança ": Controller" é OBRIGATÓRIA para usar View(), Json(), RedirectToAction()
    public class AccountController : Controller
    {
        private readonly IUsuarioApiClient _usuarioService;

        public AccountController(IUsuarioApiClient usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                // 1. Tenta autenticar na API
                var usuarioLogado = await _usuarioService.AuthenticateAsync(model.Email, model.Password);

                if (usuarioLogado == null)
                {
                    ModelState.AddModelError("", "Email ou senha inválidos.");
                    return View(model);
                }

                // 2. Monta a identidade do usuário (O Crachá)
                // Nota: Usamos '?' e '??' para evitar erro se vier nulo da API
                var role = usuarioLogado.TipoUsuario?.DescricaoTipoUsuario ?? "Comum";
                var unidadeId = usuarioLogado.UnidadeId?.ToString() ?? "";

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuarioLogado.nome),
                    new Claim(ClaimTypes.Email, usuarioLogado.email),
                    new Claim(ClaimTypes.Role, role),
                    new Claim("UnidadeId", unidadeId)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // 3. Cria o Cookie de Login (Isso resolve o Login Loop!)
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddHours(1)
                    });

                // 4. Redirecionamento Inteligente
                if (role == "Administrador" || role == "Admin")
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else if (role == "Professor" || role == "Ong")
                {
                    // Se tiver ID da unidade, manda pro painel filtrado
                    if (int.TryParse(unidadeId, out int id))
                    {
                        return RedirectToAction("Index", "PainelOng", new { unidadeId = id });
                    }
                    // Fallback se não tiver unidade
                    return RedirectToAction("Index", "Home");
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao conectar com o servidor: " + ex.Message);
                return View(model);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}