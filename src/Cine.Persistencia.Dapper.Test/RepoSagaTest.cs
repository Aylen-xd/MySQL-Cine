using Cine.Core.Persistencia;
using Cine.Persistencia.Dapper.Repos;

namespace Cine.Persistencia.Dapper.Test;

public class RepoSagaTest : TestBase
{
 readonly IRepoSaga repo;
 public RepoSagaTest () : base() => repo = new RepoSaga (Conexion);
 [Fact]
 public void TraerSagaOK()
    {
        var repos = repo.TraerElementos();
        Assert.Contains(repos, sag => sag.Nombre == "Frozen una aventura congelada");
    }

}
