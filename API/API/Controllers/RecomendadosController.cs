using Abstracciones.API;
using Abstracciones.BW;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecomendadosController : ControllerBase, IListaRecomendadoAPI
    {
        private IListaRecomendadoBW _listaRecomendadoBW;

        public RecomendadosController(IListaRecomendadoBW listaRecomendadoBW)
        {
            _listaRecomendadoBW = listaRecomendadoBW;
        }

        [HttpPost]
        [Route("crearListaRecomendado")]

        public async Task<ActionResult> crearLista(ListaRecomendado listaRecomendado)
        {
            var result = await _listaRecomendadoBW.crearLista(listaRecomendado);
            return CreatedAtAction(nameof(ObtenerListaEspecifica), new { Id = result }, listaRecomendado);
        }

        [HttpPut]
        [Route("actualizarListaRecomendado")]

        public async Task<ActionResult> actualizarLista(ListaRecomendado listaRecomendado)
        {
            var existe = await _listaRecomendadoBW.ObtenerListaEspecifica(listaRecomendado.Id);

            if (existe == null)
                return NotFound();
            return Ok(await _listaRecomendadoBW.actualizarLista(listaRecomendado));
        }

        [HttpDelete]
        [Route("eliminarListaRecomendado/{id}")]
        public async Task<ActionResult> eliminarLista(Guid id)
        {
            var existe = await _listaRecomendadoBW.ObtenerListaEspecifica(id);
            if (existe == null)
                return NotFound();

            await _listaRecomendadoBW.eliminarLista(id);
            return NoContent();
        }

        [HttpGet]
        [Route("obtenerListasRecomendado")]
        public async Task<ActionResult> obtenerListas()
        {
            return Ok(await _listaRecomendadoBW.obtenerListas());
        }

        [HttpGet]
        [Route("obtenerListasEspecifica/{id}")]
        public async Task<ActionResult> ObtenerListaEspecifica(Guid id)
        {
            return Ok(await _listaRecomendadoBW.ObtenerListaEspecifica(id));
        }
    }
}
