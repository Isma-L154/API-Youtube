using Abstracciones.Modelo;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.BC
{
    public interface IAutenticacionBC
    {
        Task<Token> LoginAync(Login login);
    }
}
