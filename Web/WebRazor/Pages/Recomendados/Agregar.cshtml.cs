using BC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Abstracciones.Modelos;

namespace WebRazor.Pages.Recomendados
{
    public class AgregarListaRecomendadoModel : PageModel
    {
        private Configuracion _configuracion;


        public AgregarListaRecomendadoModel(Configuracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public ListaRecomendado lista { get; set; } = default!;

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            string endpoint = _configuracion.ObtenerEndPoint("ListasRecomendado", "crearListaRecomendado");
            var client = new HttpClient();

            var answer = await client.PostAsJsonAsync(endpoint, lista);
            answer.EnsureSuccessStatusCode();
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnGet()
        {
            return Page();
        }
    }
}
