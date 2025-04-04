using Abstracciones.DA;
using Abstracciones.Modelos;
using Dapper;
using Helpers;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace DA
{
    public class ListaRecomendadoDA : IListaRecomendadoDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public ListaRecomendadoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorioDapper();
        }

        public async Task<Guid> actualizarLista(ListaRecomendado listaRecomendado)
        {
            string query = @"SP_ACTUALIZAR_LISTA_RECOMENDADA";
            var resultado  = await _sqlConnection.ExecuteAsync(query, new
            {
                Id = listaRecomendado.Id,
                Nombre = listaRecomendado.Nombre,
                Descripcion = listaRecomendado.Descripcion
            });
            return listaRecomendado.Id;
        }

        public async Task<Guid> crearLista(ListaRecomendado listaRecomendado)
        {
            string query = @"SP_CREAR_LISTA_RECOMENDADA";
            await _sqlConnection.QueryAsync<Abstracciones.Entidades.ListaRecomendado>(query,
            new
            {
                    Nombre = listaRecomendado.Nombre,
                    Descripcion = listaRecomendado.Descripcion
                });

            return listaRecomendado.Id;
        }

        public async Task<Guid> eliminarLista(Guid id)
        {
            string query = @"SP_ELIMINAR_LISTA_RECOMENDADA";
            int resultado = await _sqlConnection.ExecuteAsync(query, new { Id = id });
            return id;
        }

        public async Task<ListaRecomendado> ObtenerListaEspecifica(Guid id)
        {
            string query = @"SP_BUSCAR_LISTA_RECOMENDADO";
            var consulta = await _sqlConnection.QueryAsync<Abstracciones.Entidades.ListaRecomendado>(query, new { Id = id });
            return ConvertirAModelo(consulta.First());
        }

        public async Task<IEnumerable<ListaRecomendado>> obtenerListas()
        {
            string query = @"SP_OBTENER_LISTA_RECOMENDADO";
            var consulta = await _sqlConnection.QueryAsync<Abstracciones.Entidades.ListaRecomendado>(query);
            return ConvertirListasAModelo(consulta);
        }


        #region Convertidores
        private IEnumerable<Abstracciones.Modelos.ListaRecomendado> ConvertirListasAModelo(IEnumerable<Abstracciones.Entidades.ListaRecomendado> listaRecomendados)
        {
            var resultadoConversion = Convertidor.ConvertirLista<Abstracciones.Entidades.ListaRecomendado, Abstracciones.Modelos.ListaRecomendado>(listaRecomendados);
            return resultadoConversion;
        }
        private Abstracciones.Modelos.ListaRecomendado ConvertirAModelo(Abstracciones.Entidades.ListaRecomendado listaRecomendados)
        {
            var resultadoConversion = Convertidor.Convertir<Abstracciones.Entidades.ListaRecomendado, Abstracciones.Modelos.ListaRecomendado>(listaRecomendados);
            return resultadoConversion;
        }
        #endregion
    }
}
