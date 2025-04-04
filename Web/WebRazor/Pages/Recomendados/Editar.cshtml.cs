using BC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Abstracciones.Modelos;
using System.Net.Http;
using System.Text.Json;

namespace WebRazor.Pages.Recomendados
{
    public class Editar : PageModel
    {
        private Configuracion _configuracion;


        public Editar(Configuracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public ListaRecomendado lista { get; set; } = default!;

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            string endpoint = _configuracion.ObtenerEndPoint("ListasRecomendado", "actualizarListaRecomendado");
            var client = new HttpClient();

            var answer = await client.PutAsJsonAsync(endpoint, lista);
            answer.EnsureSuccessStatusCode();
            return RedirectToPage("./Index");
        }
        public async Task<ActionResult> OnGet(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            try
            {
                var _httpClient = new HttpClient();

                string endPoint = _configuracion.ObtenerEndPoint("ListasRecomendado", "obtenerListasEspecifica");
                var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endPoint, id));
                var respuesta = await _httpClient.SendAsync(solicitud);
                respuesta.EnsureSuccessStatusCode();
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                lista = JsonSerializer.Deserialize<ListaRecomendado>(resultado, options);
                return Page();
            }
            catch (Exception ex)
            {
                return Page();
            }


        }
    }
}
