using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.BW
{
    public interface IVideoBW
    {
        Task<Video> ObtenerporId(string id);
        Task<IEnumerable<Video>> Obtener(string criterio);
    }
}
