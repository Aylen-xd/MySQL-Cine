
namespace Cine.Persistencia.Dapper.Repos;

public class RepoEstudio:RepoBase, IRepoEstudio
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


    private static string queryExisteEstudio = @"SELECT IdEstudio, Nombre, Fundacion 
                                                FROM Estudio 
                                                where IdEstudio = @indiceSimple,
                                                Nombre = @indiceSimple,
                                                Fundacion = @indiceSimple";
    public Estudio? Detalle(byte elindiceSimple)
    {
        Conexion.Execute(queryExisteEstudio, new { indiceSimple = elindiceSimple });
        var BusquedaEstudio = Conexion.Query<Estudio>(queryExisteEstudio);
        return BusquedaEstudio;
    }

    public async Task DetalleAsync(byte indiceSimple)
    {
        //falta completar
    }


    /*mtd query de peliculas segun estudio y genero*/
    public List<Pelicula> mtdPeliculaEstudio(string elgenero, string elestudio)
    {
        var query = @"select Pelicula.nombre, restrincion, descripcion, Pelicula.duracion, Director_General 
                        from Produccion
                        join Pelicula using (idProduccion)
                        join Trailer using (idPelicula)
                        join Genero using (idGenero)
                        join Estudio using (idEstudio)
                        where genero = @genero and Estudio.nombre = @nombre";

        try
        {
            Conexion.Execute(query, new { genero = elgenero, nombre = elestudio });
            var peliEstudioGenero = Conexion.Query<Pelicula>(query);
            return (List<Pelicula>)peliEstudioGenero;
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public void Borrar(byte idEstudio)
    {
        var query = "DELETE FROM Estudio WHERE IdEstudio = @IdEstudio";
        /*try ejecuta la query*/
        try
        {
            Conexion.Execute(query, new { IdEstudio = idEstudio });
        }
        /*Se consulta si la excepcion contiene el mensaje del trigger y se lo alamacena en la ConstraintException ese mensaje.*/
        catch (Exception e)
        {
            if(e.Message.Contains("No se puede eliminar el estudio"))
                throw new ConstraintException(e.Message, e);
        }
    }

}


