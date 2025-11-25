using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transformese.Web.Services;

namespace Transformese.Web.Controllers
{
    public class TipoUsuariosController : Controller
    {
        private readonly ApiService _service;
        public TipoUsuariosController(ApiService service) { _service = service; }

        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAsync<List<object>>("api/tipousuarios");
            return View(list);
        }
    }
}
