using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.API
{
    public interface IListaRecomendadoAPI
    {
        [HttpPost]
        public Task<ActionResult> crearLista(ListaRecomendado listaRecomendado);

        [HttpPut]
        public Task<ActionResult> actualizarLista(ListaRecomendado listaRecomendado);

        [HttpDelete]
        public Task<ActionResult> eliminarLista(Guid id);

        [HttpGet]
        public Task<ActionResult> obtenerListas();

        [HttpGet]
        public Task<ActionResult> ObtenerListaEspecifica(Guid id);
    }
}
