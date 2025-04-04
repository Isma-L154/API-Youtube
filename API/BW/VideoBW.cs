using Abstracciones.BW;
using Abstracciones.DA;
using Abstracciones.Modelos;
using Abstracciones.SG;

namespace BW
{
    public class VideoBW : IVideoBW
    {
        private IListaRecomendadoDA _listaRecomendadoDA;
        private IVideoSG _videoSG;

        public VideoBW(IListaRecomendadoDA listaRecomendadoDA, IVideoSG videoSG)
        {
            _listaRecomendadoDA = listaRecomendadoDA;
            _videoSG = videoSG;
        }

        public Task<IEnumerable<Video>> Obtener(string criterio)
        {
            return _videoSG.Obtener(criterio);
        }


        public Task<Video> ObtenerporId(string id)
        {
            return _videoSG.ObtenerporId(id);
        }   
    }
}
