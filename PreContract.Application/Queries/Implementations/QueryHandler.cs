using Contracts.Api.Application.Queries.Interfaces;
using Contracts.Api.Application.Queries.ViewModels;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Contracts.Api.Application.Queries.Querys
{
    [ExcludeFromCodeCoverage]
    public class QueryHandler : IQueryHandler
    {
        readonly string _connectionString;

        public QueryHandler(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<string> BuildParametersXml(Dictionary<string, object> parameters)
        {
            await Task.FromResult(0);

            var element = new XElement("Record",
                parameters.Select(kv => new XElement(kv.Key.Trim().ToLower(), kv.Value == null ? null : kv.Value.ToString().Trim())));

            return element.ToString().Replace("'", "''");
        }

        public async Task<IEnumerable<T>> Search<T>(string procedure, string parametersXml, string sort)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@pit_parametrosXML", parametersXml, DbType.String);
                parameters.Add("@piv_orderBy", sort, DbType.String);

                return await connection.QueryAsync<T>(procedure, parameters, commandTimeout:10000 ,commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<(IEnumerable<dynamic>, int)> FindAll<T>(string procedure, string parametersXml, int paginaActual, int cantidadMostrar, string sort)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@pit_parametrosXML", parametersXml, DbType.String);
                parameters.Add("@pii_paginaActual", paginaActual, DbType.String);
                parameters.Add("@pii_cantidadMostrar", cantidadMostrar, DbType.String);
                parameters.Add("@piv_orderBy", sort, DbType.String);

                parameters.Add("@poi_totalRegistros", 0, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

                var list = await connection.QueryAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
                var quantity = parameters.Get<int>("@poi_totalRegistros");

                return (list, quantity);
            }
        }

        public async Task<IEnumerable<T>> Job<T>(string procedure)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return await connection.QueryAsync<T>(procedure, commandType: CommandType.StoredProcedure, commandTimeout: 300);
                //var quantity = parameters.Get<int>("@poi_totalRegistros");

                //return list;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(string procedure, string parametersXml, Pagination pagination)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@pit_parametrosXML", parametersXml, DbType.String);
                parameters.Add("@pii_paginaActual", pagination.PageIndex, DbType.Int32);
                parameters.Add("@pii_cantidadMostrar", pagination.PageSize, DbType.Int32);
                parameters.Add("@piv_orderBy", pagination.SortDirection, DbType.String);
                parameters.Add("@poi_totalRegistros", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var result = await connection.QueryAsync<T>(procedure, parameters, commandType: CommandType.StoredProcedure);

                pagination.Total = parameters.Get<int>("@poi_totalRegistros");

                return result;
            }
        }

        public async Task<IEnumerable<T>> Job<T>(string procedure, string parametersXml)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@pit_parametrosXML", parametersXml, DbType.String);
                parameters.Add("@piv_orderBy", string.Empty, DbType.String);

                return await connection.QueryAsync<T>(procedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
