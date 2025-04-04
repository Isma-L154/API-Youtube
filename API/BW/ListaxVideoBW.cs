using Abstracciones.BW;
using Abstracciones.DA;
using Abstracciones.Modelos;
using Abstracciones.SG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW
{
    public class ListaxVideoBW : IListaxVideoBW
    {
        private IListaxVideoDA _listaxVideoDA;
        private IVideoSG _videoSG;

        public ListaxVideoBW(IListaxVideoDA listaxVideoDA, IVideoSG videoSG)
        {
            _listaxVideoDA = listaxVideoDA;
            _videoSG = videoSG;
        }

        public async Task<Guid> AnadirVideo(ListaxVideo ListaxVideo)
        {
            return await _listaxVideoDA.AnadirVideo(ListaxVideo);
        }

        public async  Task<string> EliminarVideo(string id)
        {
            return await _listaxVideoDA.EliminarVideo(id);
        }

        public async Task<IEnumerable<ListaxVideo>> ObtenerListaxVideo()
        {
            return await _listaxVideoDA.ObtenerListaxVideo();
        }

        public async Task<IEnumerable<Video>> ObtenerListaxVideo(Guid IdLista)
        {
            var videoIds = await _listaxVideoDA.ObtenerListaxVideo(IdLista);
            List<Video> videosYoutube = new List<Video>();
            // Consultar la API para cada video ID y obtener los detalles completos
            foreach (var videoId in videoIds)
            {
                var videoDetails = await _videoSG.ObtenerporId(videoId);
                if (videoDetails != null)
                {
                    videosYoutube.Add(videoDetails);
                }
            }
            return videosYoutube;
        }
    }
}
