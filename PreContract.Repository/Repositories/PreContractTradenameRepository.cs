using PreContracts.Api.Domain.Aggregates.PreContractTradenameAggregate;
using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PreContracts.Api.Repository.Repositories
{
	public class PreContractTradenameRepository : IPreContractTradenameRepository
	{
		readonly string _connectionString = string.Empty;

		public PreContractTradenameRepository() { }

		public PreContractTradenameRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		public async Task<int> Register(PreContractTradename preContractTradename)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();

				try
				{
					var parameters = new DynamicParameters();

					parameters.Add("@poi_contract_tradename_id", preContractTradename.contract_tradename_id, DbType.Int32, ParameterDirection.InputOutput);
					parameters.Add("@pii_contract_id", preContractTradename.contract_id, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pii_contract_version", preContractTradename.contract_version, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pii_contract_modification", preContractTradename.contract_modification, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pii_tradename_id", preContractTradename.tradename_id, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pii_rubric_id", preContractTradename.rubric_id, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pii_register_user_id", preContractTradename.register_user_id, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@piv_register_user_fullname", preContractTradename.register_user_fullname, DbType.String, ParameterDirection.Input);
					parameters.Add("@pid_register_datetime", preContractTradename.register_datetime, DbType.DateTime, ParameterDirection.Input);
					parameters.Add("@pii_update_user_id", preContractTradename.update_user_id, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@piv_update_user_fullname", preContractTradename.update_user_fullname, DbType.String, ParameterDirection.Input);
					parameters.Add("@pid_update_datetime", preContractTradename.update_datetime, DbType.DateTime, ParameterDirection.Input);
					parameters.Add("@pii_state", preContractTradename.state, DbType.Int32, ParameterDirection.Input);

					var result = await connection.ExecuteAsync(@"CONTRACT.adv_t_pre_contract_tradename_insert_update", parameters, commandType: CommandType.StoredProcedure);

					preContractTradename.contract_tradename_id = parameters.Get<int>("@poi_contract_tradename_id");

					return preContractTradename.contract_tradename_id;
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}
			}
		}

		public async Task<int> RegisterJson(List<PreContractTradename> preContractTradenames, SqlTransaction transaction)
		{
			var parameters = new DynamicParameters();

			parameters.Add("jsonData", JsonConvert.SerializeObject(preContractTradenames), dbType: DbType.String);
			var result = await transaction.Connection.ExecuteAsync("contract.adv_t_pre_contract_tradename_insert_update_json", parameters, transaction, commandType: CommandType.StoredProcedure);

			return result;
		}
	}
}
