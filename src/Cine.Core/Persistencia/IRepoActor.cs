namespace Cine.Core.Persistencia;

public interface IRepoActor: IRepoAlta<Actor>, IListado<Actor>,
                            IRepoAltaAsync<Actor>, IListadoAsync<Actor>
{

}

