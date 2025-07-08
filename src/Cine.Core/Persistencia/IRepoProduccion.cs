namespace Cine.Core.Persistencia;

public interface IRepoProduccion : IRepoAlta<Produccion>, IListado<Produccion>, IRepoAltaAsync<Produccion>, IListadoAsync<Produccion>
{
    IEnumerable<Produccion> DirectorActualiza(Produccion actualizacionProduc, byte unidProduccion);

    Task<IEnumerable<Produccion>> DirectorActualizaAsync(Produccion actualizacionProduc, byte unidProduccion);
}
