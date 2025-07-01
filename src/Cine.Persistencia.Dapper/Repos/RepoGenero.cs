namespace Cine.Persistencia.Dapper.Repos;

public class RepoGenero : RepoBase, IRepoGenero
{
    public RepoGenero(IDbConnection conexion)
        : base(conexion) { }

    private static DynamicParameters ParametrosAlta(Genero genero)
    {
        //Vamos a declara la lista de params
        var parametros = new DynamicParameters();
        parametros.Add("unidGenero", direction: ParameterDirection.Output);
        parametros.Add("ungenero", genero.Nombre);

        //Conexion.Execute("InsGenero", parametros);

        //Voy a tomar el valor output
        //genero.IdGenero = parametros.Get<byte>("unidGenero");
        return parametros;
    }

    public void Alta(Genero elemento)
    {
        DynamicParameters parametros = ParametrosAlta(elemento);

        Conexion.Execute("InsGenero", parametros);

        elemento.IdGenero = parametros.Get<byte>("xidGenero");
    }

    //-------------------------------------------Metodo async----------------------------------------------
    public async Task AltaAsync(Genero elemento)
    {
        DynamicParameters parametros = ParametrosAlta(elemento);

        await Conexion.ExecuteAsync("InsGenero", parametros);

        elemento.IdGenero = parametros.Get<byte>("xidGenero");
    }
    //------------------------------------------------------------------------------------


    private static string queryTraerElementos = "SELECT * FROM Genero";

    public IEnumerable<Genero> TraerElementos()
    {
        var generos = Conexion.Query<Genero>(queryTraerElementos);
        return generos;
    }

    //------------------------ Metodo Async TraerElementos -----------------------------

    public async Task<IEnumerable<Genero>> TraerElementosAsync()
    {
        var genero = await Conexion.QueryAsync<Genero>(queryTraerElementos);
        return genero;
    }
    //------------------------------------------------------------------------------------

}
