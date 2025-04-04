using Abstracciones.API;
using Abstracciones.BW;
using Abstracciones.Modelo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase, IUsuarioController
    {
        private IUsuarioBW _usuarioBW;

        public UsuarioController(IUsuarioBW usuarioBW)
        {
            _usuarioBW = usuarioBW;
        }
        [Authorize(Roles = "1")] //Se deja con el Rol 1 por el momento que es el Rol de Cliente en BD
        [HttpGet("ObtenerUsuario")]
        public async Task<IActionResult> ObtenerUsuario([FromBody] UserRequest usuario)
        {
            return Ok(await _usuarioBW.ObtenerUsuario(usuario));
        }
        
        [AllowAnonymous] //Permisos para todos de realizar el registro
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Usuario usuario)
        {
            return Ok(await _usuarioBW.CrearUsuario(usuario));
        }
    }
}
