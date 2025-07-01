using System.Threading.Tasks;
using Cine.Persistencia.Dapper.Repos;

namespace Cine.Persistencia.Dapper;

public class RepoTrailer : RepoBase, IRepoTrailer
{
    public RepoTrailer(IDbConnection conexion) : base(conexion)
    {
    }

    public static DynamicParameters ParametrosAlta(Trailer trailer)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unidTrailer", direction: ParameterDirection.Output);
        parametros.Add("unidPelicula", trailer.IdPelicula);
        parametros.Add("unidGenero", trailer.IdGenero);
        parametros.Add("unnombre", trailer.Nombre);
        parametros.Add("unaduracion", trailer.Duracion);

        //Conexion.Execute("InsTrailer", parametros);

        //trailer.IdTrailer = parametros.Get<byte>("unidTrailer");
        return parametros;
    }

    public void Alta(Trailer elemento)
    {
        DynamicParameters parametros = ParametrosAlta(elemento);

        Conexion.Execute("InsTrailer", parametros);

        elemento.IdTrailer = parametros.Get<byte>("xidTrailer");
    }

    //-------------------------------------------Metodo async----------------------------------------------
    public async Task AltaAsync(Trailer elemento)
    {
        DynamicParameters parametros = ParametrosAlta(elemento);

        await Conexion.ExecuteAsync("InsTrailer", parametros);

        elemento.IdTrailer = parametros.Get<byte>("xidTrailer");
    }
    //------------------------------------------------------------------------------------

    public IEnumerable<Trailer> TraerElementos()
    {
        var trailer = Conexion.Query<Trailer>(queryTraerElementos);
        return trailer;
    }

    //-------------------------------------------Metodo async TrarElementos----------------------------------------------
    public static string queryTraerElementos = @"SELECT * FROM Trailer";
    public async Task<IEnumerable<Trailer>> TraerElementosAsync()
    {
        var Trailer = await Conexion.QueryAsync<Trailer>(queryTraerElementos);
        return Trailer;
    }
    //------------------------------------------------------------------------------------

}

