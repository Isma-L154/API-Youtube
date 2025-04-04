using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.API
{
    public interface IVideoController
    {
        public Task<IActionResult> ObtenerporId(string id);
        public Task<IActionResult> Obtener(string criterio);
    }
}
