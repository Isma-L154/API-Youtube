using Abstracciones.API;
using Abstracciones.BW;
using Abstracciones.Entidades;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListaxVideoController : ControllerBase, IListaxVideoAPI
    {
        private IListaxVideoBW _listaxVideoBW;

        public ListaxVideoController(IListaxVideoBW listaxVideoBW)
        {
            _listaxVideoBW = listaxVideoBW;
        }

        [HttpPost]
        public async Task<IActionResult> AnadirVideo(Abstracciones.Modelos.ListaxVideo ListaxVideo)
        {
           await _listaxVideoBW.AnadirVideo(ListaxVideo);
           return CreatedAtAction(nameof(ObtenerListaxVideo), ListaxVideo);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarVideo(string id)
        {
            await _listaxVideoBW.EliminarVideo(id);
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerListaxVideo()
        {
            return Ok(await _listaxVideoBW.ObtenerListaxVideo());
        }

        [HttpGet("ObtenerEspecifico/{IdLista}")]
        public async Task<IActionResult> ObtenerListaxVideo([FromRoute] Guid IdLista)
        {
            return Ok(await _listaxVideoBW.ObtenerListaxVideo(IdLista));
        }
    }
}
