using NucleoLoguin_API.Models;
using Microsoft.EntityFrameworkCore;

namespace NucleoLoguin_API.Service;

public class AutomovelService
{
    private readonly ApiContext db;
    public AutomovelService(ApiContext apiContext)
    {
        db = apiContext;
    }

    public List<Automovel> listar()
    {
        return db.automoveis.ToList();
    }
    public Automovel listar(int id)
    {
        var veiculoBuscado = db.automoveis.ToList().Where(x => x.Id == id).FirstOrDefault();
        if (veiculoBuscado != null)
            return veiculoBuscado;

        return null;
    }
    public void salvar(Automovel automovel)
    {
        db.Add(automovel);
        db.SaveChanges();
    }
    public bool alterar(Automovel updateAutomovel)
    {
        var oldAutomovel = db.automoveis.ToList().Where(x => updateAutomovel.Id == x.Id).FirstOrDefault();
        oldAutomovel.Nome = updateAutomovel.Nome;
        oldAutomovel.NomeFabricante = updateAutomovel.NomeFabricante;
        oldAutomovel.Ano = updateAutomovel.Ano;
        db.automoveis.Update(oldAutomovel);
        db.SaveChanges();
        return true;
    }
    public void deletar(int id)
    {
        var automovel = db.automoveis.ToList().Where(x => x.Id == id).FirstOrDefault();
        if (automovel != null)
            db.automoveis.Remove(automovel);

        db.SaveChanges();
    }
}
