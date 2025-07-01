
namespace Cine.Persistencia.Dapper.Repos;

public class RepoEstudio : RepoBase, IRepoEstudio
{
    private string queryEstudio;

    public RepoEstudio(IDbConnection conexion)
        : base(conexion) { }

    private static DynamicParameters ParametrosAlta(Estudio estudio)
    {
        var parametros = new DynamicParameters();
        parametros.Add("xidEstudio", direction: ParameterDirection.Output);
        parametros.Add("xnombre", estudio.Nombre);
        parametros.Add("xfundacion", estudio.Fundacion);

        //Conexion.Execute("InsEstudio", parametros);

        //estudio.IdEstudio = parametros.Get<byte>("xidEstudio");    
        return parametros;
    }

    public void Alta(Estudio elemento)
    {
        DynamicParameters parametros = ParametrosAlta(elemento);

        Conexion.Execute("insEstudio", parametros);

        elemento.IdEstudio = parametros.Get<byte>("idEstudio");
    }

    //-------------------------------------------Metodo async----------------------------------------------
    public async Task AltaAsync(Estudio elemento)
    {
        DynamicParameters parametros = ParametrosAlta(elemento);

        await Conexion.ExecuteAsync("insEstudio", parametros);

        elemento.IdEstudio = parametros.Get<byte>("idEstudio");
    }
    //------------------------------------------------------------------------------------


    //se declara como variable para ser usada despues por los metodos que la necesitan 
    private static string queryTraerElementos = @"SELECT * FROM Estudio";

    public IEnumerable<Estudio> TraerElementos()
    {
        var estudios = Conexion.Query<Estudio>(queryTraerElementos);
        return estudios;
    }

    //------------------------ Metodo Async TraerElementos -----------------------------
    public async Task<IEnumerable<Estudio>> TraerElementosAsync()
    {
        var estudios = await Conexion.QueryAsync<Estudio>(queryTraerElementos);
        return estudios;
    }
    //------------------------------------------------------------------------------------


    private static string queryExisteEstudio = @"SELECT IdEstudio, Nombre, Fundacion 
                                                FROM Estudio 
                                                where IdEstudio = @indiceSimple,
                                                Nombre = @indiceSimple,
                                                Fundacion = @indiceSimple";
    public Estudio? Detalle(byte elindiceSimple)
    {
        Conexion.Execute(queryExisteEstudio, new { indiceSimple = elindiceSimple });
        var BusquedaEstudio = Conexion.Query<Estudio>(queryExisteEstudio);
        return (Estudio?)BusquedaEstudio;
    }

    //------------------------ Metodo Async Detalle -----------------------------
    public async Task<Estudio> DetalleAsync(byte elindiceSimple) //Devuelve un tipo task, si o si devuelve algo
    {
        Conexion.ExecuteAsync(queryExisteEstudio, new { indiceSimple = elindiceSimple });
        var BusquedaEstudio = await Conexion.QueryAsync<Estudio>(queryExisteEstudio);
        return (Estudio)BusquedaEstudio;
    }
    //------------------------------------------------------------------------------------


    private static string PeliGeneroEstudio = @"select Pelicula.nombre, restrincion, descripcion,   
                                                Pelicula.duracion, Director_General 
                                                from Produccion
                                                join Pelicula using (idProduccion)
                                                join Trailer using (idPelicula)
                                                join Genero using (idGenero)
                                                join Estudio using (idEstudio)
                                                where genero = @genero and Estudio.nombre = @nombre";

    /*mtd query de peliculas segun estudio y genero*/
    public List<Pelicula> mtdPeliculaEstudio(string elgenero, string elestudio)
    {
        try
        {
            Conexion.Execute(PeliGeneroEstudio, new { genero = elgenero, nombre = elestudio });
            var peliEstudioGenero = Conexion.Query<Pelicula>(PeliGeneroEstudio);
            return (List<Pelicula>)peliEstudioGenero;
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    //------------------------ Metodo Async Lista -----------------------------
    public async Task<List<Pelicula>> mtdPeliculasEstudioAsync(string elgenero, string elestudio)
    {
        try
        {
            Conexion.ExecuteAsync(PeliGeneroEstudio, new { genero = elgenero, nombre = elestudio });
            var peliEstudioGenero = await Conexion.QueryAsync<Pelicula>(PeliGeneroEstudio);
            return (List<Pelicula>)peliEstudioGenero;
        }
        catch (System.Exception)
        {
            throw;
        }
    }
    //------------------------------------------------------------------------------------

    private static string BorrarQuery = "DELETE FROM Estudio WHERE IdEstudio = @IdEstudio";

    public void Borrar(byte elidEstudio)
    {
        /*try ejecuta la query*/
        try
        {
            Conexion.Execute(BorrarQuery, new { IdEstudio = elidEstudio });
        }
        /*Se consulta si la excepcion contiene el mensaje del trigger y se lo alamacena en la ConstraintException ese mensaje.*/
        catch (Exception e)
        {
            if (e.Message.Contains("No se puede eliminar el estudio"))
                throw new ConstraintException(e.Message, e);
        }
    }

    //------------------------ Metodo Async Borrar -----------------------------
    public async Task BorrarAsync(byte elidEstudio)
    {
        try
        {
            await Conexion.ExecuteAsync(BorrarQuery, new { IdEstudio = elidEstudio });
        }
        /*Se consulta si la excepcion contiene el mensaje del trigger y se lo alamacena en la ConstraintException ese mensaje.*/
        catch (Exception e)
        {
            if (e.Message.Contains("No se puede eliminar el estudio"))
                throw new ConstraintException(e.Message, e);
        }
    }
    //------------------------------------------------------------------------------------

}


