using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelo
{
    public class UserRequest
    {
        [Required]
        public string NombreUsuario { get; set; }
        [Required]
        [EmailAddress]
        public string CorreoElectronico { get; set; }
    }
}
