namespace Cine.Core.Persistencia;

public interface IRepoEstudio : IRepoAlta<Estudio>, IListado<Estudio>, IRepoDetalle<Estudio, byte>, IRepoAltaAsync<Estudio>, IListadoAsync<Estudio>
{
    /*Metodo que implementa el repo para Borrar.*/
    void Borrar(byte idestudio);

    Task BorrarAsync(byte idestudio);
}


