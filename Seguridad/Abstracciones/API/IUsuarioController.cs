using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.API
{
    public interface IUsuarioController
    {
        Task<IActionResult> PostAsync([FromBody] Modelo.Usuario usuario); //Post para registrar un usuario
        Task<IActionResult> ObtenerUsuario([FromBody] Modelo.UserRequest usuario); //Get para validar la obtencion de un Usuario
    }
}
