namespace Cine.Core.Persistencia;

public interface IRepoProduccion : IRepoAlta<Produccion>, IListado<Produccion>, IRepoAltaAsync<Produccion>
{
    IEnumerable<Produccion> DirectorActualiza(Produccion actualizacionProduc, byte unidProduccion);
}
