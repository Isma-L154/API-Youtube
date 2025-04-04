using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Abstracciones.DA
{
    public interface IListaRecomendadoDA
    {
        public Task<Guid> crearLista(ListaRecomendado listaRecomendado);
        public Task<Guid> actualizarLista(ListaRecomendado listaRecomendado);
        public Task<Guid> eliminarLista(Guid id);
        public Task<IEnumerable<ListaRecomendado>> obtenerListas();
        public Task<ListaRecomendado> ObtenerListaEspecifica(Guid id);

    }
}
