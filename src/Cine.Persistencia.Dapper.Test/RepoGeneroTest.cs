using System.Threading.Tasks;
using Cine.Core;
using Cine.Core.Persistencia;
using Cine.Persistencia.Dapper.Repos;

namespace Cine.Persistencia.Dapper.Test;

public class RepoGeneroTest : TestBase
{
    readonly IRepoGenero repo;
    public RepoGeneroTest() : base()
        => repo = new RepoGenero(Conexion);

    [Fact]
    public void TraerGenerosOK() //listo
    {
        var repos = repo.TraerElementos();
        Assert.Contains(repos, gen => gen.Nombre == "Ficcion" && gen.IdGenero == 1);
    }

    [Fact]

    public void AltageneroOK() //--listo
    {
        string nombre = "Terror";

        var altageneroterror = new Genero()
        {
            Nombre = nombre
        };

        repo.Alta(altageneroterror);
    }

    //------------------------ Tests Async -------------------------------------
    [Fact]
    public async Task TraerGenerosOKAsync()
    {
        var repos = await repo.TraerElementoAsync();
        Assert.Contains(repos, gen => gen.Nombre == "Ficcion" && gen.IdGenero == 1);
    }
    
    [Fact]
    public async Task AltageneroOKAync()
    {
        string nombre = "Terror";

        var altageneroterror = new Genero()
        {
            Nombre = nombre
        };

        await repo.AltaAsync(altageneroterror);
    }
}

