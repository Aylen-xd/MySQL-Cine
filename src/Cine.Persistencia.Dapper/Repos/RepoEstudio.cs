namespace Cine.Persistencia.Dapper.Repos;

public class RepoEstudio:RepoBase, IRepoEstudio
{
    private string queryEstudio;

    public RepoEstudio(IDbConnection conexion)
        : base(conexion) { }

    public void Alta(Estudio estudio)
    {
        var parametros = new DynamicParameters();
        parametros.Add("xidEstudio", ParameterDirection.Output);
        parametros.Add("xnombre", estudio.Nombre);
        parametros.Add("xfundacion", estudio.Fundacion);

        Conexion.Execute("InsEstudio", parametros);

        estudio.IdEstudio = parametros.Get<byte>("xidEstudio");    
    }

    public IEnumerable<Estudio> TraerElementos()
    {
        var query = @"SELECT * FROM Estudio";
        var estudios = Conexion.Query<Estudio>(query);
        return estudios;    
    }

    public Estudio? ObtenerEstudio(byte IdEstudio)
    {
          using (var multi = Conexion.QueryMultiple(sql: queryEstudio, new { id = IdEstudio}))
          {
            var estudio = multi.ReadSingleOrDefault<Estudio>();
            if (estudio is not null)
            {
                estudio.Producciones = multi.Read<Produccion>().ToList();
            }

            return estudio;
          }
    }

    public Estudio? Detalle(byte indiceSimple)
    {
        throw new NotImplementedException();
    }
}


