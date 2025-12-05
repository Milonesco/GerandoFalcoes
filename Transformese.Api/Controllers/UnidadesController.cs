using Microsoft.AspNetCore.Mvc;
using Transformese.Domain.Entities;
using Transformese.Data.Repositories; // Importante
using System.Collections.Generic;

namespace Transformese.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadesController : ControllerBase
    {
        // Agora usamos o Repository, não o DbContext direto
        private readonly UnidadeParceiraRepository _repository;

        // Injeta o Repositório (Talvez precise configurar no Program.cs, veja nota abaixo)
        public UnidadesController(UnidadeParceiraRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Post([FromBody] UnidadeParceira unidade)
        {
            _repository.Adicionar(unidade);
            return Ok(unidade);
        }

        [HttpGet]
        public ActionResult<List<UnidadeParceira>> Get()
        {
            return _repository.ObterTodas();
        }
    }
}