namespace Cine.Persistencia.Dapper.Repos;

public class RepoSaga : RepoBase, IRepoSaga
{
    public RepoSaga(IDbConnection conexion) : base(conexion)
    {
    }

    public static DynamicParameters ParametrosAlta(Saga saga)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unidsaga", direction: ParameterDirection.Output);
        parametros.Add("unNumero_Saga", saga.NSaga);
        parametros.Add("unidpelicula", saga.IdPelicula);
        parametros.Add("unnombre", saga.NombreSaga);

        //Conexion.Execute("insSaga", parametros);

        //saga.IdSaga = parametros.Get<byte>("unidsaga");
        return parametros;
    }

    public void Alta(Saga elemento)
    {
        DynamicParameters parametros = ParametrosAlta(elemento);

        Conexion.Execute("InsSaga", parametros);

        elemento.IdSaga = parametros.Get<byte>("xidSaga");
    }

    //-------------------------------------------Metodo async----------------------------------------------
    public async Task AltaAsync(Saga elemento)
    {
        DynamicParameters parametros = ParametrosAlta(elemento);

        await Conexion.ExecuteAsync("InsSaga", parametros);

        elemento.IdSaga = parametros.Get<byte>("xidSaga");
    }


    public IEnumerable<Saga> TraerElementos()
    {
        var query = @"SELECT * FROM Saga";
        var saga = Conexion.Query<Saga>(query);
        return saga;
    }
}
