using Abstracciones.BW;
using Abstracciones.DA;
using Abstracciones.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW
{
    public class UsuarioBW : IUsuarioBW
    {
        private IUsuarioDA _usuarioDA;

        public UsuarioBW(IUsuarioDA usuarioDA)
        {
            _usuarioDA = usuarioDA;
        }
        public async Task<Guid> CrearUsuario(Usuario usuario)
        {
            return await _usuarioDA.CrearUsuario(usuario);
        }

        public async Task<Usuario> ObtenerUsuario(UserRequest usuario)
        {
            return await _usuarioDA.ObtenerUsuario(usuario);
        }
    }
}
