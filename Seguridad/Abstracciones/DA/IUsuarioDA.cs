using Abstracciones.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.DA
{
    public interface IUsuarioDA
    {
        //Estos dos que estan iguales que en el Middleware lo dejamos por si se necesita dentro de este API
        Task<Usuario> ObtenerUsuario(UserRequest usuario);

        Task<IEnumerable<Perfil>> ObtenerPerfilesxUsuario(UserRequest usuario);

        //Este es el Task que vamos a usar para registrar
        Task<Guid> CrearUsuario(Usuario usuario);
    }
}
