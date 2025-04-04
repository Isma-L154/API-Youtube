using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class Video
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Url { get; set; }
        public string Descripción { get; set; }
        public string Canal { get; set; }
        public DateTime Fecha { get; set; }
        public string Miniatura { get; set; }
    }
}
