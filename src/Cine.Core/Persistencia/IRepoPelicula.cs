namespace Cine.Core.Persistencia;

public interface IRepoPelicula : IRepoAlta<Pelicula>, IListado<Pelicula>, IRepoAltaAsync<Pelicula>, IListadoAsync<Pelicula>
{
    IEnumerable<Actor> ActoresPelicula(byte idPelicula);

    Task<IEnumerable<Actor>> ActoresPeliculasAsync(byte idPelicula);
}
