using Abstracciones.Modelos;
using BC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace WebRazor.Pages.Videos
{
    public class DetalleModel : PageModel
    {
        private Configuracion _configuracion;

        public DetalleModel(Configuracion configuracion)
        {
            _configuracion = configuracion;
        }
        public Video video { get; set; } = default!;
        public async Task<ActionResult> OnGet(string? id)
        {
            if (id == null)
                return NotFound();

            string endPoint = _configuracion.ObtenerEndPoint("Videos", "ObtenerVideo");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get,string.Format(endPoint,id));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            video = JsonSerializer.Deserialize<Video>(resultado, options);
            return Page();
        }
    }
}
