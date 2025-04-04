using Abstracciones.Modelos;


namespace Abstracciones.BW
{
    public interface IListaRecomendadoBW
    {
        public Task<Guid> crearLista(ListaRecomendado listaRecomendado);
        public Task<Guid> actualizarLista(ListaRecomendado listaRecomendado);
        public Task<Guid> eliminarLista(Guid id);
        public Task<IEnumerable<ListaRecomendado>> obtenerListas();
        public Task<ListaRecomendado> ObtenerListaEspecifica(Guid id);
    }
}
