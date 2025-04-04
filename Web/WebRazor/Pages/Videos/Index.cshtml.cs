using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography.X509Certificates;
using Abstracciones.Modelos;

using System.Text.Json.Serialization;
using System.Text.Json;
using BC;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Collections.Generic;
using System.Security.Claims;


namespace WebRazor.Pages.Videos
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private Configuracion _configuracion;
        private HttpClient _httpClient = new HttpClient();


        public IndexModel(Configuracion configuracion)
        {
            _configuracion = configuracion;
        }
        public string Rol { get; set; }
        [BindProperty]
        public string IdVideo { get; set; }
        [BindProperty]
        public Guid ListId { get; set; }
        public ListaxVideos ListaxVideos { get; set; }
        public IList<ListaRecomendado> listas { get; set; } = default!;
        public IList<Video> videos { get; set; } = default!;

        [HttpGet("{id}")]
        public async Task OnGet(string id)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            Rol = userRole;
            try
            {
                string endPoint = _configuracion.ObtenerEndPoint("Videos", "ObtenerVideos");
                var url = endPoint.Replace("{0}", id);

                var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(url,id));
                var respuesta = await _httpClient.SendAsync(solicitud);
                respuesta.EnsureSuccessStatusCode();
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                videos = JsonSerializer.Deserialize<List<Video>>(resultado, options);
            }
            catch (Exception ex) {
                return;
            }
            
            try
            {
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

        public async Task<IActionResult> OnPost()
        {
            if (IdVideo == null || ListId == Guid.Empty)
                return BadRequest("ID de video o lista no proporcionado.");

            var listaxVideo = new ListaxVideos
            {
                IdVideo = IdVideo,
                IdLista = ListId
            };

            string endPoint = _configuracion.ObtenerEndPoint("ListaxVideos", "AnadirVideos");
            var cliente = new HttpClient();
            var respuesta = await cliente.PostAsJsonAsync<ListaxVideos>(endPoint, listaxVideo);
            respuesta.EnsureSuccessStatusCode();      

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
