using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using aula14.Models;
using System.Text.Json;
using System.Security.Cryptography.X509Certificates;

namespace aula14.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        List<Atividade> listaAtividades = new List<Atividade>();
        string DADOS = "./Dados/dados.json";
        string dadosJson = System.IO.File.ReadAllText(DADOS);
        if (dadosJson.Length != 0)
            listaAtividades = JsonSerializer.Deserialize<List<Atividade>>(dadosJson);
        return View(listaAtividades);
    }

    [HttpPost]
    public IActionResult Criar([FromForm] string descricao, [FromForm] string prioridade)
    {
        List<Atividade> listaAtividades = new List<Atividade>();
        string DADOS = "./Dados/dados.json";
        string dadosJson = System.IO.File.ReadAllText(DADOS);
        if (dadosJson.Length != 0)
            listaAtividades = JsonSerializer.Deserialize<List<Atividade>>(dadosJson);

        Atividade novaAtividade = new Atividade(descricao, Enum.Parse<Prioridade>(prioridade));

        listaAtividades.Add(novaAtividade);
        SalvarAtividades(listaAtividades); 

        return RedirectToAction("Index");
    }

    private void SalvarAtividades(List<Atividade> listaAtividades)
    {
        string DADOS = "./Dados/dados.json";
        string dadosJson = JsonSerializer.Serialize(listaAtividades);
        System.IO.File.WriteAllText(DADOS, dadosJson);
    }

    public IActionResult Excluir(string Id)
    {
        // Carregar atividades existentes
        List<Atividade> atividades = new List<Atividade>();
        string DADOS = "./Dados/dados.json";
        string dadosJson = System.IO.File.ReadAllText(DADOS);
        if (dadosJson.Length != 0)
            atividades = JsonSerializer.Deserialize<List<Atividade>>(dadosJson);

        // Procurar a atividade a ser excluida
        var atividadeASerExcluida = atividades.FirstOrDefault(a => a.Id == Id);

        if (atividadeASerExcluida != null)
        {
            atividades.Remove(atividadeASerExcluida);
            SalvarAtividades(atividades);
        }

        return RedirectToAction("Index");
    }
    
    public IActionResult ConcluirTarefa(string Id)
    {
        // Carregar atividades existentes
        List<Atividade> atividades = new List<Atividade>();
        string DADOS = "./Dados/dados.json";
        string dadosJson = System.IO.File.ReadAllText(DADOS);
        if (dadosJson.Length != 0)
            atividades = JsonSerializer.Deserialize<List<Atividade>>(dadosJson);

        // Procurar a atividade a ser excluida
        var atividadeASerConcluida = atividades.FirstOrDefault(a => a.Id == Id);

        if (atividadeASerConcluida != null)
        {
            atividadeASerConcluida.Concluida = true;
            SalvarAtividades(atividades);
        }
        
        return RedirectToAction("Index");        
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
