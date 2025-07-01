

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

    //-------------------------------------------Metodo async----------------------------------------------
    public async Task AltaAsync(Produccion elemento)
    {
        DynamicParameters parametros = ParametrosAlta(elemento);

        await Conexion.ExecuteAsync("InsProduccion", parametros);

        elemento.IdProduccion = parametros.Get<byte>("xidProduccion");
    }


    public IEnumerable<Produccion> TraerElementos()
    {
        var query = @"SELECT * FROM Produccion";
        var producciones = Conexion.Query<Produccion>(query);
        return producciones;
    }

    public IEnumerable<Produccion> DirectorActualiza(Produccion produccion, byte unidProduccion)
    {
        var Query = @"UPDATE Produccion
                    set Director_General = unDirector, Productor = unProductor, Guion = unGuion, Musica = unaMusica, Presupuesto = unPresuppuesto, Sonido = unSonido, Vestuario = unVestuario
                    WHERE idProduccion = @idProduccion";
           
        var actualizaciones = Conexion.Query<Produccion>(Query, new {idProduccion = produccion});
        return actualizaciones;
    }
}
