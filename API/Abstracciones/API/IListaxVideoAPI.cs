using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.API
{
    public interface IListaxVideoAPI
    {
        public Task<IActionResult> ObtenerListaxVideo();
        public Task<IActionResult> ObtenerListaxVideo(Guid IdLista);
        public Task<IActionResult> EliminarVideo(string id);
        public Task<IActionResult> AnadirVideo(ListaxVideo ListaxVideo);
    }
}
