namespace Cine.Core.Persistencia;

public interface IRepoPelicula: IRepoAlta<Pelicula>, IListado<Pelicula>, IRepoAltaAsync<Pelicula>
{
    IEnumerable<Actor> ActoresPelicula (byte idPelicula);
}
