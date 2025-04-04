using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelo
{
    
    public class Token
    {
        public bool ValidacionExitosa { get; set; } //Conocer si fue exitosa la Val
        public string AccessToken { get; set; } //Token
    }

    public class TokenConfiguracion
    {
        //Todos estos son datos para el token para irlo rellenando
        [Required]
        [StringLength(100, MinimumLength = 32)] //Key con JWT debe ser minimo de 32 al generarse y max 100
        public string key { get; set; }
        [Required]
        public string Issuer { get; set; } //Quien es el que genera el Token
        public string Audience { get; set; }
        [Required]
        public double Expires { get; set; } // Tiene que tener un tiempo de expiracion
    }
}
