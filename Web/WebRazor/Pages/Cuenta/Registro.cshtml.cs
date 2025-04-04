using Abstracciones.Modelos;
using BC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace WebRazor.Pages.Cuenta
{
    public class RegistroModel : PageModel
    {
        private Configuracion _configuracion;

        public RegistroModel(Configuracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public Usuario usuario { get; set; } = default!;
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var Hash = Password.GenerarHash(usuario.Contrasenia);
            usuario.PasswordHash = Password.ObtenerHash(Hash);
            usuario.IdServicio = Guid.Parse(_configuracion.GetPropiedad("Servicio"));


            string endPoint = _configuracion.ObtenerEndPoint("Seguridad","AgregarUsuario");
            var cliente = new HttpClient();
            var respuesta = await cliente.PostAsJsonAsync<Usuario>(string.Format(endPoint), usuario);
            respuesta.EnsureSuccessStatusCode();
            var Resultado = JsonConvert.DeserializeObject<Guid>(respuesta.Content.ReadAsStringAsync().Result);

            return RedirectToPage("../index");
        }
    }
}
