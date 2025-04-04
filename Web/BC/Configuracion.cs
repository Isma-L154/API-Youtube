using Abstracciones.Modelos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC
{
    public class Configuracion
    {
        private readonly IConfiguration _configuration;

        public Configuracion(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ObtenerEndPoint(string seccion, string nombreEndPoint)
        {
            var apiEndPoints = _configuration.GetSection("APIEndPoints").Get<APIEndPointSection>();

            List<APIEndPoint> endPointsList;

            switch (seccion)
            {
                case "Seguridad":
                    endPointsList = apiEndPoints.Seguridad;
                    break;
                case "Videos":
                    endPointsList = apiEndPoints.Videos;
                    break;
                case "ListasRecomendado":
                    endPointsList = apiEndPoints.ListasRecomendado;
                    break;
                case "ListaxVideos":
                    endPointsList = apiEndPoints.ListaxVideos;
                    break;
                default:
                    throw new ArgumentException("Sección no encontrada");
            }

            return endPointsList?.FirstOrDefault(e => e.Nombre == nombreEndPoint)?.Valor;
        }
        public string GetPropiedad(string propiedad)
        {
            return _configuration.GetValue<string>(propiedad);
        }
    }
}
