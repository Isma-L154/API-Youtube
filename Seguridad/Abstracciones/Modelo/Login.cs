using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelo
{
    public class Login
    {
        [Required]
        public Guid Id { get; set; }
        public string? NombreUsuario { get; set; }
        [Required]
        public string PasswordHash { get; set; }

        [EmailAddress]
        public string CorreoElectronico { get; set; }
        public Guid IdServicio { get; set; }
    }
}
