using Abstracciones.BC;
using Abstracciones.DA;
using Abstracciones.Modelo;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

//Regla para Generar el Token 
namespace BC
{
    public class AutenticacionBC : IAutenticacionBC
    {
        public IConfiguration _configuration;
        public IUsuarioDA _usuarioDA;

        public AutenticacionBC(IConfiguration configuration, IUsuarioDA usuarioDA)
        {
            _configuration = configuration;
            _usuarioDA = usuarioDA;
        }
        public async Task<Token> LoginAync(Login login)
        {
            Token respuestaToken = new Token() { AccessToken = string.Empty, ValidacionExitosa = false };
            var resultadoVerificacionCredenciales = await VerificarLoginAsync(login);
            if (!resultadoVerificacionCredenciales){
                return respuestaToken;
            }
            TokenConfiguracion tokenConfiguracion = _configuration.GetSection("Token").Get<TokenConfiguracion>();
            JwtSecurityToken token = await GenerarTokenJWT(login, tokenConfiguracion);
            respuestaToken.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
            respuestaToken.ValidacionExitosa = true;
            return respuestaToken;
        }

        private async Task<bool> VerificarLoginAsync(Login login)
        {
            //Para la validar que el usuario y la contraseña coincidan
            var usuario = await _usuarioDA.ObtenerUsuario(new UserRequest { NombreUsuario = login.NombreUsuario, CorreoElectronico = login.CorreoElectronico });
            return (login != null && login.PasswordHash == usuario.PasswordHash);
        }

        private async Task<JwtSecurityToken> GenerarTokenJWT(Login login, TokenConfiguracion tokenConfiguracion)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfiguracion.key));
            List<Claim> claims = await GenerarClaims(login);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); //Credenciales para sellar el Key que generamos arriba
            
            //Aqui generamos el Token JWT para luego devolverlo, Aqui esto lo estamos tomando de la informacion que agarramos en el AppSettings
            var token = new JwtSecurityToken(tokenConfiguracion.Issuer, tokenConfiguracion.Audience, claims, expires: DateTime.Now.AddMinutes(tokenConfiguracion.Expires), signingCredentials: credentials);
            return token;
        }


        private async Task<List<Claim>> GenerarClaims(Login login)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("usuario", login.NombreUsuario));
            claims.Add(new Claim("servicio", login.IdServicio.ToString()));
            claims.Add(new Claim("usuarioId", login.Id.ToString())); //Agregamos el ID como Claim dentro del Token JWT
            var perfiles = await ObtenerPerfiles(login);
            foreach (var perfil in perfiles)
            {
                claims.Add(new Claim(ClaimTypes.Role, perfil.Id.ToString()));
            }
            return claims;
        }

        private async Task<IEnumerable<Perfil>> ObtenerPerfiles(Login login)
        {
            return await _usuarioDA.ObtenerPerfilesxUsuario(new UserRequest { NombreUsuario = login.NombreUsuario, CorreoElectronico = login.CorreoElectronico });
        }
    }
}
