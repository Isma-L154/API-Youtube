using Abstracciones.BW;
using Abstracciones.DA;
using Abstracciones.Entidades;
using Abstracciones.Modelos;
using Dapper;
using Helpers;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class ListaxVideoDA : IListaxVideoDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public ListaxVideoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorioDapper();
        }
        public async Task<Guid> AnadirVideo(Abstracciones.Modelos.ListaxVideo ListaxVideo)
        {
            string query = @"AnadirVideo";
            await _sqlConnection.QueryAsync<Abstracciones.Entidades.ListaxVideo>(query,
            new
            {
                IdLista = ListaxVideo.IdLista,
                IdVideo = ListaxVideo.IdVideo
            });

            return ListaxVideo.Id;
        }

        public async Task<string> EliminarVideo(string id)
        {
            string query = $"DELETE FROM ListaxVideo WHERE IdVideo = '{id}'";
            await _sqlConnection.ExecuteAsync(query, new { IdVideo = id });
            return id;
        }

        public async Task<IEnumerable<Abstracciones.Modelos.ListaxVideo>> ObtenerListaxVideo()
        {
            string query = @"SELECT * FROM ListaxVideo";
            var consulta = await _sqlConnection.QueryAsync<Abstracciones.Entidades.ListaxVideo>(query);
            return ConvertirListasAModelo(consulta);
        }

        public async Task<IEnumerable<string>> ObtenerListaxVideo(Guid IdLista)
        {
            string query = "SELECT IdVideo FROM ListaxVideo WHERE IdLista = @IdLista";
            var videoIds = await _sqlConnection.QueryAsync<string>(query, new { IdLista });
            return videoIds;
        }

        #region Convertidores
        private IEnumerable<Abstracciones.Modelos.ListaxVideo> ConvertirListasAModelo(IEnumerable<Abstracciones.Entidades.ListaxVideo> ListaxVideo)
        {
            var resultadoConversion = Convertidor.ConvertirLista<Abstracciones.Entidades.ListaxVideo, Abstracciones.Modelos.ListaxVideo>(ListaxVideo);
            return resultadoConversion;
        }
        private Abstracciones.Modelos.ListaxVideo ConvertirAModelo(Abstracciones.Entidades.ListaxVideo ListaxVideo)
        {
            var resultadoConversion = Convertidor.Convertir<Abstracciones.Entidades.ListaxVideo, Abstracciones.Modelos.ListaxVideo>(ListaxVideo);
            return resultadoConversion;
        }
        #endregion
    }
}
