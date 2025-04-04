using Abstracciones.Modelos;
using Abstracciones.Modelos.VideoDetalle;
using Abstracciones.SG;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace SG
{
    public class VideoSG : IVideoSG
    {

        public IConfiguration _configuration;

        public VideoSG(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Video>> Obtener(string criterio)
        {
            string endPoint = _configuration.GetSection("APIEndPoints").Get<List<APIEndPoint>>().Where(e => e.Nombre == "ObtenerVideos").First().Valor;
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endPoint, criterio));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var videoAPI = JsonConvert.DeserializeObject<VideoAPI>(resultado);
            var resultadoVideos = new List<Video>();
            foreach (var item in videoAPI.items.Where(v=> v.id.kind== "youtube#video"))
            {
                resultadoVideos.Add(
                    new Video()
                    {
                        Id = item.id.videoId,
                        Nombre= item.snippet.title,
                        Descripción= item.snippet.description,
                        Url= @"$item.id.videoId",
                        Canal= item.snippet.channelTitle,
                        Fecha=item.snippet.publishTime,
                        Miniatura = item.snippet.thumbnails.high.url
                    }
                    );
            }
            return resultadoVideos;
        }

        public async Task<Video> ObtenerporId(string id)
        {
            string endPoint = _configuration.GetSection("APIEndPoints").Get<List<APIEndPoint>>().Where(e => e.Nombre == "ObtenerVideo").First().Valor;
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endPoint, id));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var videoAPI = JsonConvert.DeserializeObject<VideoAPIDetalle>(resultado);
            var resultadoVideos = new List<Video>();
            foreach (var item in videoAPI.items)
            {
                resultadoVideos.Add(
                    new Video()
                    {
                        Id = item.id,
                        Nombre = item.snippet.title,
                        Descripción = item.snippet.description,
                        Url = @"$item.id.videoId",
                        Canal = item.snippet.channelTitle,
                        Fecha = item.snippet.publishedAt,
                        Miniatura=item.snippet.thumbnails.high.url
                    }
                    );
            }
            return resultadoVideos.FirstOrDefault();
        }
    }
}
