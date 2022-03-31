using Microsoft.AspNetCore.Mvc;
using NucleoLoguin_API.Models;
using NucleoLoguin_API.Service;

namespace NucleoLoguin_API.Controllers
{
    [Produces("application/json")]
    //[Route("api/Automoveis")]
    public class AutomovelController : Controller
    {
        private readonly ApiContext _context;
        public AutomovelService _service;
        public AutomovelController(ApiContext context)
        {
            _context = context;
            _service = new AutomovelService(_context);
        }

        [HttpPost]
        [Route("api/Automoveis")]

        public async Task<IActionResult> Post([FromBody]Automovel automovel)
        {
            var novoAutomovel = new Automovel 
            { 
                Nome = automovel.Nome, 
                NomeFabricante = automovel.NomeFabricante, 
                Ano = automovel.Ano 
            };
            _service.salvar(novoAutomovel);
            return Ok("sucesso");
        }

        [HttpGet]
        [Route("api/Automoveis")]
        public IActionResult Get()
        {
            var resposta = _service.listar();
            return Ok(resposta);
        }

        [HttpPut]
        [Route("api/Automoveis")]
        public IActionResult Put([FromBody]Automovel updateAutomovel)
        {
            _service.alterar(updateAutomovel);
            return Ok();
        }
        [HttpDelete]
        [Route("api/Automoveis/{id}")]
        public IActionResult Delete(int id)
        {
            _service.deletar(id);
            return Ok();
        }
    }
}
