using Abstracciones.DA;
using Abstracciones.Modelo;
using Dapper;
using Helpers;
using System.Data.SqlClient;

namespace DA
{
    public class UsuarioDA : IUsuarioDA
    {
        IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public UsuarioDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorioDapper();
        }
        public async Task<Guid> CrearUsuario(Usuario usuario)
        {
            var sql = @"[AgregarUsuario]";
            var resultado = await _sqlConnection.ExecuteScalarAsync<Guid>(sql, new { NombreUsuario = usuario.NombreUsuario, PasswordHash = usuario.PasswordHash, CorreoElectronico = usuario.CorreoElectronico });
            return resultado;
        }

        public async Task<IEnumerable<Perfil>> ObtenerPerfilesxUsuario(UserRequest usuario)
        {
            string sql = @"[ObtenerPerfilesxUsuario]";
            var consulta = await _sqlConnection.QueryAsync<Abstracciones.Entidad.Perfil>(sql, new { CorreoElectronico = usuario.CorreoElectronico, NombreUsuario = usuario.NombreUsuario });
            return Convertidor.ConvertirLista<Abstracciones.Entidad.Perfil, Abstracciones.Modelo.Perfil>(consulta);
        }

        public async Task<Usuario> ObtenerUsuario(UserRequest usuario)
        {
            string sql = @"[ObtenerUsuario]";
            var consulta = await _sqlConnection.QueryAsync<Abstracciones.Entidad.Usuario>(sql, new { CorreoElectronico = usuario.CorreoElectronico, NombreUsuario = usuario.NombreUsuario });
            return Convertidor.Convertir<Abstracciones.Entidad.Usuario, Abstracciones.Modelo.Usuario>(consulta.FirstOrDefault());
        }
    }
}
