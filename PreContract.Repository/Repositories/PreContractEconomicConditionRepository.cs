using PreContracts.Api.Domain.Aggregates.PreContractEconomicConditionAggregate;
using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace PreContracts.Api.Repository.Repositories
{
	public class PreContractEconomicConditionRepository : IPreContractEconomicConditionRepository
	{
		readonly string _connectionString = string.Empty;

		public PreContractEconomicConditionRepository()
		{
		}

		public PreContractEconomicConditionRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		public async Task<int> Register(PreContractEconomicCondition preContractEconomicCondition)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();

				try
				{
					var parameters = new DynamicParameters();

					parameters.Add("@poi_economic_condition_id", preContractEconomicCondition.economic_condition_id, DbType.Int32, ParameterDirection.InputOutput);
					parameters.Add("@pid_commission", preContractEconomicCondition.commission, DbType.Decimal, ParameterDirection.Input);
					parameters.Add("@pii_category_id", preContractEconomicCondition.category_id, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pii_state", preContractEconomicCondition.state, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pii_contract_id", preContractEconomicCondition.contract_id, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pii_contract_version", preContractEconomicCondition.contract_version, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pii_contract_modification", preContractEconomicCondition.contract_modification, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pii_register_user_id", preContractEconomicCondition.register_user_id, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@piv_register_user_fullname", preContractEconomicCondition.register_user_fullname, DbType.String, ParameterDirection.Input);
					parameters.Add("@pid_register_datetime", preContractEconomicCondition.register_datetime, DbType.DateTime, ParameterDirection.Input);
					parameters.Add("@pii_update_user_id", preContractEconomicCondition.update_user_id, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@piv_update_user_fullname", preContractEconomicCondition.update_user_fullname, DbType.String, ParameterDirection.Input);
					parameters.Add("@pid_update_datetime", preContractEconomicCondition.update_datetime, DbType.DateTime, ParameterDirection.Input);

					var result = await connection.ExecuteAsync(@"CONTRACT.adv_t_pre_contract_economic_condition_insert_update", parameters, commandType: CommandType.StoredProcedure);

					preContractEconomicCondition.economic_condition_id = parameters.Get<int>("@poi_economic_condition_id");

					return preContractEconomicCondition.economic_condition_id;
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}
			}
		}

		public async Task<int> RegisterJson(List<PreContractEconomicCondition> preContractVariableCommissionRanges, SqlTransaction transaction)
		{
			var parameters = new DynamicParameters();

			parameters.Add("jsonData", JsonConvert.SerializeObject(preContractVariableCommissionRanges), dbType: DbType.String);
			var result = await transaction.Connection.ExecuteAsync("contract.adv_t_pre_contract_economic_condition_insert_update_json", parameters, transaction, commandType: CommandType.StoredProcedure);

			return result;
		}
	}
}
