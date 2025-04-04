using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Entidades
{
    public class ListaRecomendado
    {
        public Guid Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int estado { get; set; }
        }

        public class ListaRecomendadoRespuesta
        {
            public ListaRecomendado? info { get; set; }
            public List<ListaRecomendado>? listas { get; set; }
            public string Codigo { get; set; }
            public string Mensaje { get; set; }
    }
}
