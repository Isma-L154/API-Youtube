using Abstracciones.BW;
using Abstracciones.DA;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BW
{
    public class ListaRecomendadoBW : IListaRecomendadoBW
    {
        private IListaRecomendadoDA _listaRecomendadoDA;

        public ListaRecomendadoBW(IListaRecomendadoDA listaRecomendadoDA)
        {
            _listaRecomendadoDA = listaRecomendadoDA;
        }

        public async Task<Guid> actualizarLista(ListaRecomendado listaRecomendado)
        {
            return await _listaRecomendadoDA.actualizarLista(listaRecomendado);
        }

        public async Task<Guid> crearLista(ListaRecomendado listaRecomendado)
        {
            return await _listaRecomendadoDA.crearLista(listaRecomendado);
        }

        public async Task<Guid> eliminarLista(Guid id)
        {
            return await _listaRecomendadoDA.eliminarLista(id);
        }

        public async Task<ListaRecomendado> ObtenerListaEspecifica(Guid id)
        {
            return await _listaRecomendadoDA.ObtenerListaEspecifica(id);
        }

        public async Task<IEnumerable<ListaRecomendado>> obtenerListas()
        {
            return await _listaRecomendadoDA.obtenerListas();
        }
    }
}
