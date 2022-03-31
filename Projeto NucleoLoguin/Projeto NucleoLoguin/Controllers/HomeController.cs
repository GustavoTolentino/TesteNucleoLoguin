using Microsoft.AspNetCore.Mvc;
using NucleoLoguin_API.Models;
using Projeto_NucleoLoguin.Models;
using System.Diagnostics;

namespace Projeto_NucleoLoguin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static HttpClient client = new HttpClient();
        public ListaAutomoveis listaAutomoveis;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            listaAutomoveis = new ListaAutomoveis();
            listaAutomoveis.automoveis = new List<Automovel>();
        }

        public IActionResult Index()
        {
            return View("Index", listaAutomoveis);
        }

        public IActionResult Privacy()
        {
            return View("Index", listaAutomoveis);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> salvarAsync()
        {
            String inputName = Request.Form["inputName"];
            String inputFabricante = Request.Form["inputFabricante"];
            String inputAno = Request.Form["inputAno"];
            String exampleCheck = Request.Form["exampleCheck"];
            SalvarVeiculo(inputName, inputFabricante, inputAno);

            listaAutomoveis.automoveis = await BuscarVeiculos();
            return View("Index", listaAutomoveis);
        }
        private static async Task SalvarVeiculo(String inputName, String inputFabricante, String inputAno)
        {
            Automovel automovel = new Automovel { Nome = inputName, NomeFabricante = inputFabricante, Ano = int.Parse(inputAno.ToString()) };
            HttpResponseMessage response2 = await client.PostAsJsonAsync("http://localhost:5145/api/Automoveis", automovel);
        }
        [HttpPost]
        public async Task<IActionResult> atualizar()
        {
            String inputId = Request.Form["inputIdUpdate"];
            String inputNome = Request.Form["inputNameUpdate"];
            String inputFabricante = Request.Form["inputFabricanteUpdate"];
            String inputAno = Request.Form["inputAnoUpdate"];
            await AtualizarVeiculo(inputId, inputNome, inputFabricante, inputAno);

            listaAutomoveis.automoveis = await BuscarVeiculos();
            return View("Index", listaAutomoveis);
        }
        private static async Task AtualizarVeiculo(String id, String inputNome, String inputFabricante, String inputAno)
        {
            Automovel automovel = new Automovel { Id = (short)int.Parse(id), Nome = inputNome, NomeFabricante = inputFabricante, Ano = int.Parse(inputAno.ToString()) };
            HttpResponseMessage response = await client.PutAsJsonAsync("http://localhost:5145/api/Automoveis", automovel);
        }
        [HttpPost]
        public async Task<IActionResult> deletar()
        {
            String inputIdDelete = Request.Form["inputIdDelete"];
            await ExcluirVeiculo(inputIdDelete);
            return View("Index", listaAutomoveis);
        }
        private static async Task ExcluirVeiculo(String inputIdDelete)
        {
            HttpResponseMessage response = await client.DeleteAsync("http://localhost:5145/api/Automoveis/" + inputIdDelete);
        }

        [HttpPost]
        public async Task<IActionResult> listarAsync()
        {
            listaAutomoveis.automoveis = await BuscarVeiculos();
            return View("Index", listaAutomoveis);
        }

        private async static Task<List<Automovel>> BuscarVeiculos()
        {
            List<Automovel> lisAutomoveis = new List<Automovel>();
            lisAutomoveis = await client.GetAsync("http://localhost:5145/api/Automoveis").Result.Content.ReadFromJsonAsync<List<Automovel>>();
            return lisAutomoveis;
        }
    }
}