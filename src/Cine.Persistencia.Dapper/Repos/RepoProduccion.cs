

using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Cine.Persistencia.Dapper.Repos;

public class RepoProduccion : RepoBase, IRepoProduccion
{
    public RepoProduccion(IDbConnection conexion) : base(conexion)
    {
    }

    public static DynamicParameters ParametrosAlta(Produccion produccion)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unidProduccion", direction: ParameterDirection.Output);
        parametros.Add("unidEstudio", produccion.IdEstudio);
        parametros.Add("unDirector_General", produccion.Director);
        parametros.Add("unGuion", produccion.Guion);
        parametros.Add("unProductor", produccion.Productor);
        parametros.Add("unVestuario", produccion.Vestuario);
        parametros.Add("unSonido", produccion.Sonido);
        parametros.Add("unPresupuesto", produccion.Presupuesto);
        parametros.Add("unaMusica", produccion.Musica);

        //Conexion.Execute("InsProduccion", parametros);

        //produccion.IdProduccion = parametros.Get<byte>("unidProduccion");

        return parametros;
    }

    public void Alta(Produccion elemento)
    {
        DynamicParameters parametros = ParametrosAlta(elemento);

        Conexion.Execute("InsProduccion", parametros);

        elemento.IdProduccion = parametros.Get<byte>("xidProduccion");
    }

    //-------------------------------------------Metodo async Alta----------------------------------------------
    public async Task AltaAsync(Produccion elemento)
    {
        DynamicParameters parametros = ParametrosAlta(elemento);

        await Conexion.ExecuteAsync("InsProduccion", parametros);

        elemento.IdProduccion = parametros.Get<byte>("xidProduccion");
    }
    //------------------------------------------------------------------------------------

    public static string queryTraerElementos = "SELECT * FROM Produccion";
    public IEnumerable<Produccion> TraerElementos()
    {
        var producciones = Conexion.Query<Produccion>(queryTraerElementos);
        return producciones;
    }

    //-------------------------------------------Metodo async TraerElementos---------------------------------------------

    public async Task<IEnumerable<Produccion>> TraerElementosAsync()
    {
        var producciones = await Conexion.QueryAsync<Produccion>(queryTraerElementos);
        return producciones;
    }

    //------------------------------------------------------------------------------------

    public static string queryDirectorActualiza = @"UPDATE Produccion
                    set Director_General = unDirector, Productor = unProductor, Guion = unGuion, Musica = unaMusica, Presupuesto = unPresuppuesto, Sonido = unSonido, Vestuario = unVestuario
                    WHERE idProduccion = @idProduccion";
    public IEnumerable<Produccion> DirectorActualiza(Produccion produccion, byte unidProduccion)
    {
        var actualizaciones = Conexion.Query<Produccion>(queryDirectorActualiza, new { idProduccion = produccion });
        return actualizaciones;
    }

    //-------------------------------------------Metodo async DirectorActualiza---------------------------------------------
    public async Task<IEnumerable<Produccion>> DirectorActualizaAsync(Produccion produccion, byte unidProduccion)
    {
        var actualizaciones = await Conexion.QueryAsync<Produccion>(queryDirectorActualiza, new { idProduccion = produccion });
        return actualizaciones;
    }
    //------------------------------------------------------------------------------------
}
