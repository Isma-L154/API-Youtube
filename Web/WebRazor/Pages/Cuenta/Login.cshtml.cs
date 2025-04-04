using Abstracciones.Modelos;
using BC;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace WebRazor.Pages.Cuenta
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Login loginInfo { get; set; } = default!;
        [BindProperty]
        public Token token { get; set; }

        public Configuracion _configuracion;

        public LoginModel(Configuracion configuracion)
        {
            _configuracion = configuracion;
        }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost() {
            if (ModelState.IsValid)
            {
                var Hash = Password.GenerarHash(loginInfo.Contrasenia);
                loginInfo.PasswordHash=Password.ObtenerHash(Hash);
                loginInfo.IdServicio = Guid.Parse(_configuracion.GetPropiedad("Servicio"));
                loginInfo.nombreUsuario = loginInfo.CorreoElectronico.Split("@")[0];

                string endPoint = _configuracion.ObtenerEndPoint("Seguridad", "Login");
                var cliente = new HttpClient();
                var respuesta = await cliente.PostAsJsonAsync<LoginRequest>(string.Format(endPoint), new LoginRequest { CorreoElectronico = loginInfo.CorreoElectronico, nombreUsuario = loginInfo.nombreUsuario, IdServicio = loginInfo.IdServicio, PasswordHash = loginInfo.PasswordHash });
                respuesta.EnsureSuccessStatusCode();
                token = JsonConvert.DeserializeObject<Token>(respuesta.Content.ReadAsStringAsync().Result);

                if (token.ValidacionExitosa)
                {
                    JwtSecurityToken? tokens = leerInformacionToken();
                    var perfiles = tokens.Claims.Where(c=>c.Type==ClaimTypes.Role);
                    await AgregarClaims(tokens, perfiles);
                    return RedirectToPage("/index");
                }
            }
            return Page();
        }
        private JwtSecurityToken? leerInformacionToken()
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token.AccessToken);
            var tokens = jsonToken as JwtSecurityToken;
            return tokens;
        }

        private async Task AgregarClaims(JwtSecurityToken? tokens, IEnumerable<Claim> perfiles)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, tokens.Claims.First(c=>c.Type=="usuario").Value),
            new Claim("usuario", tokens.Claims.First(c=>c.Type=="usuario").Value),
            new Claim(ClaimTypes.Email, loginInfo.CorreoElectronico),
            new Claim("Token", token.AccessToken),
            new Claim(ClaimTypes.NameIdentifier, tokens.Claims.FirstOrDefault(c => c.Type == "usuarioId")?.Value)

        };
            foreach (var perfil in perfiles)
            {
                claims.Add(new Claim(ClaimTypes.Role, perfil.Value));
            }

            await establecerAutenticacion(claims);
        }

        private async Task establecerAutenticacion(List<Claim> claims)
        {
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);
        }
    }
}
