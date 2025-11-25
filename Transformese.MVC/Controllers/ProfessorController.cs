using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Transformese.MVC.Services;

namespace TransformeseMVC.Web.Controllers
{
    [Authorize(Roles = "Professor")]
    public class ProfessorController : Controller
    {
        private readonly ICursoApiClient _api;
        public ProfessorController(ICursoApiClient api) => _api = api;

        public async Task<IActionResult> Index()
        {
            var turmas = await _api.GetAllAsync();
            return View(turmas);
        }
    }
}
