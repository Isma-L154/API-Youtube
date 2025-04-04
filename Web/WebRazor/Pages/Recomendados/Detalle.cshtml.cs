using Abstracciones.Modelos;
using BC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Text.Json;

namespace WebRazor.Pages.Recomendados
{
    [Authorize]
    public class DetalleModel : PageModel
    {
        private Configuracion _configuracion;
        private HttpClient _httpClient = new HttpClient();


        public DetalleModel(Configuracion configuracion)
        {
            _configuracion = configuracion;
        }
        public string Rol { get; set; }
        public ListaRecomendado lista { get; set; } = default!;
        public IList<Video> videos { get; set; } = default!;

        [HttpGet("{id}")]
        public async Task<ActionResult> OnGet(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            Rol = userRole;

            string endPoint = _configuracion.ObtenerEndPoint("ListaxVideos", "ObtenerxListaEspecifico");
                var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endPoint, id));
                var respuesta = await _httpClient.SendAsync(solicitud);
                respuesta.EnsureSuccessStatusCode();
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                videos = JsonSerializer.Deserialize<List<Video>>(resultado, options);
            return Page();
        }
        public async Task<IActionResult> OnPost(string id)
        {
            if (id == null)
                return BadRequest("ID no proporcionado.");

            string endPoint = _configuracion.ObtenerEndPoint("ListaxVideos", "EliminarVideo");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Delete, string.Format(endPoint, id));
            var respuesta = await cliente.SendAsync(solicitud);

            if (respuesta.IsSuccessStatusCode)
            {
                var IdLista = Request.Form["IdLista"];
                return RedirectToPage("/Recomendados/Detalle", new { id = IdLista });
            }
            else
            {
                return Page();
            }
        }
    }
}
