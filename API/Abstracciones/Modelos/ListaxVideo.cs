using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class ListaxVideo
    {
        public Guid Id { get; set; }
        public Guid IdLista { get; set; }
        public string IdVideo { get; set; }
    }
}
