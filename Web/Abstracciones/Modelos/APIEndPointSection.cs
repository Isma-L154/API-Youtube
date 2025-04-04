using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class APIEndPointSection
    {
        public List<APIEndPoint> Seguridad { get; set; }
        public List<APIEndPoint> Videos { get; set; }
        public List<APIEndPoint> ListasRecomendado { get; set; }
        public List<APIEndPoint> ListaxVideos { get; set; }
    }
}
