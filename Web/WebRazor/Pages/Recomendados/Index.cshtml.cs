using Abstracciones.Modelos;
using BC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;

namespace WebRazor.Pages.Recomendados
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private Configuracion _configuracion;
        
        public IndexModel(Configuracion configuracion)
        {
            _configuracion = configuracion;
        }
        public string Rol { get; set; }
        public IList<ListaRecomendado> listas { get; set; } = default!;

        public async Task OnGet()
        {
            try
            {
                string userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                Rol = userRole;

                var _httpClient = new HttpClient();
                string endPoint = _configuracion.ObtenerEndPoint("ListasRecomendado", "obtenerListasRecomendado");

                var solicitud = new HttpRequestMessage(HttpMethod.Get, endPoint);
                var respuesta = await _httpClient.SendAsync(solicitud);
                respuesta.EnsureSuccessStatusCode();
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                listas = JsonSerializer.Deserialize<List<ListaRecomendado>>(resultado, options);
            }
            catch (Exception ex)
            {
                return;
            }
        }
        public async Task<IActionResult> OnPost(Guid? id)
        {
            if (id == Guid.Empty)
                return BadRequest("ID no proporcionado.");

            string endPoint = _configuracion.ObtenerEndPoint("ListasRecomendado", "eliminarListaRecomendado");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Delete, string.Format(endPoint, id));
            var respuesta = await cliente.SendAsync(solicitud);

            if (respuesta.IsSuccessStatusCode)
            {
                return RedirectToPage("/Recomendados/Index");
            }
            else
            {
                return Page();
            }
        }
    }

}
