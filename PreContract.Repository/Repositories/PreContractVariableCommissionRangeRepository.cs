using Contracts.Api.Domain.Aggregates.PreContractVariableCommissionRangeAggregate;
using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Contracts.Api.Repository.Repositories
{
	public class PreContractVariableCommissionRangeRepository : IPreContractVariableCommissionRangeRepository
	{
		readonly string _connectionString = string.Empty;

		public PreContractVariableCommissionRangeRepository() { }

		public PreContractVariableCommissionRangeRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		public async Task<int> Register(PreContractVariableCommissionRange preContractVariableCommissionRange)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();

				try
				{
					var parameters = new DynamicParameters();

					parameters.Add("@poi_contract_variable_com_range_id", preContractVariableCommissionRange.contract_variable_com_range_id, DbType.Int32, ParameterDirection.InputOutput);
					parameters.Add("@pii_contract_id", preContractVariableCommissionRange.contract_id, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pii_contract_version", preContractVariableCommissionRange.contract_version, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pii_contract_modification", preContractVariableCommissionRange.contract_modification, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pii_contract_tradename_id", preContractVariableCommissionRange.contract_tradename_id, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pii_state", preContractVariableCommissionRange.state, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pii_validity_time", preContractVariableCommissionRange.validity_time, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pib_validity_active", preContractVariableCommissionRange.validity_active, DbType.Boolean, ParameterDirection.Input);
					parameters.Add("@pid_percentage", preContractVariableCommissionRange.percentage, DbType.Decimal, ParameterDirection.Input);
					parameters.Add("@pid_validity_start_date", preContractVariableCommissionRange.validity_start_date, DbType.DateTime, ParameterDirection.Input);
					parameters.Add("@pid_validity_end_date", preContractVariableCommissionRange.validity_end_date, DbType.DateTime, ParameterDirection.Input);
					parameters.Add("@pii_grade", preContractVariableCommissionRange.grade, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pii_register_user_id", preContractVariableCommissionRange.register_user_id, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@piv_register_user_fullname", preContractVariableCommissionRange.register_user_fullname, DbType.String, ParameterDirection.Input);
					parameters.Add("@pid_register_datetime", preContractVariableCommissionRange.register_datetime, DbType.DateTime, ParameterDirection.Input);
					parameters.Add("@pii_update_user_id", preContractVariableCommissionRange.update_user_id, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@piv_update_user_fullname", preContractVariableCommissionRange.update_user_fullname, DbType.String, ParameterDirection.Input);
					parameters.Add("@pid_update_datetime", preContractVariableCommissionRange.update_datetime, DbType.DateTime, ParameterDirection.Input);
					parameters.Add("@pii_category_id", preContractVariableCommissionRange.category_id, DbType.Int32, ParameterDirection.Input);

					var result = await connection.ExecuteAsync(@"CONTRACT.adv_t_pre_contract_variable_commission_range_insert_update", parameters, commandType: CommandType.StoredProcedure);

					preContractVariableCommissionRange.contract_variable_com_range_id = parameters.Get<int>("@poi_contract_variable_com_range_id");

					return preContractVariableCommissionRange.contract_variable_com_range_id;
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}
			}
		}

		public async Task<int> RegisterJson(List<PreContractVariableCommissionRange> preContractVariableCommissionRanges, SqlTransaction transaction)
		{
			var parameters = new DynamicParameters();

			parameters.Add("jsonData", JsonConvert.SerializeObject(preContractVariableCommissionRanges), dbType: DbType.String);
			var result = await transaction.Connection.ExecuteAsync("contract.adv_t_pre_contract_variable_commission_range_insert_update_json", parameters, transaction, commandType: CommandType.StoredProcedure);

			return result;
		}
	}
}
