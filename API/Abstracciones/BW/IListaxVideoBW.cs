using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.BW
{
    public interface IListaxVideoBW
    {
        public Task<IEnumerable<ListaxVideo>> ObtenerListaxVideo();
        public Task<IEnumerable<Video>> ObtenerListaxVideo(Guid IdLista);
        public Task<string> EliminarVideo(string id);
        public Task<Guid> AnadirVideo(ListaxVideo ListaxVideo);
    }
}
