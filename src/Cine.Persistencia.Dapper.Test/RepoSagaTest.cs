using System.Threading.Tasks;
using Cine.Core;
using Cine.Core.Persistencia;
using Cine.Persistencia.Dapper.Repos;

namespace Cine.Persistencia.Dapper.Test;

public class RepoSagaTest : TestBase
{
    readonly IRepoSaga repo;

    public RepoSagaTest() : base() => repo = new RepoSaga(Conexion);

    [Fact]
    public void TraerElementosOK() //--listo
    {
        var repos = repo.TraerElementos();
        Assert.Contains(repos, sag => sag.IdPelicula == 1 && sag.IdSaga == 1);
    }

    [Fact]
    public void AltaSagaStarWars() //--listo
    {
        byte idPelicula = 1;
        byte numero_saga = 2;
        string nombre = "star wars saga";

        var altaSagaStarWars = new Saga(0, numero_saga, idPelicula, nombre)
        {
            NSaga = numero_saga,
            IdPelicula = idPelicula,
            NombreSaga = nombre
        };

        repo.Alta(altaSagaStarWars);
    }

    [Fact]
    public void AltaSagaStarWars8() //--listo
    {
        byte idPelicula = 2;
        byte numero_saga = 2;
        string nombre = "star wars saga";

        var altaSagaStarWars = new Saga(0, numero_saga, idPelicula, nombre)
        {
            NSaga = numero_saga,
            IdPelicula = idPelicula,
            NombreSaga = nombre
        };

        repo.Alta(altaSagaStarWars);
    }

    //------------------------ Tests Async -------------------------------------
    [Fact]
    public async Task TraerElementosOKAsync()
    {
        var repos = await repo.TraerElementoAsync();
        Assert.Contains(repos, sag => sag.IdPelicula == 1 && sag.IdSaga == 1);
    }

    [Fact]
    public async Task AltaSagaStarWarsAsync()
    {
        byte idPelicula = 1;
        byte numero_saga = 2;
        string nombre = "star wars saga";

        var altaSagaStarWars = new Saga(0, numero_saga, idPelicula, nombre)
        {
            NSaga = numero_saga,
            IdPelicula = idPelicula,
            NombreSaga = nombre
        };

        await repo.AltaAsync(altaSagaStarWars);
    }

    [Fact]
    public async Task AltaSagaStarWars8Async()
    {
        byte idPelicula = 2;
        byte numero_saga = 2;
        string nombre = "star wars saga";

        var altaSagaStarWars = new Saga(0, numero_saga, idPelicula, nombre)
        {
            NSaga = numero_saga,
            IdPelicula = idPelicula,
            NombreSaga = nombre
        };

        await repo.AltaAsync(altaSagaStarWars);
    }
}
