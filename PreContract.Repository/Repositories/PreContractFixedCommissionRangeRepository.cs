using PreContracts.Api.Domain.Aggregates.PreContractFixedCommissionRangeAggregate;
using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PreContracts.Api.Repository.Repositories
{
	public class PreContractFixedCommissionRangeRepository : IPreContractFixedCommissionRangeRepository
	{
		readonly string _connectionString = string.Empty;

		public PreContractFixedCommissionRangeRepository() { }

		public PreContractFixedCommissionRangeRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		public async Task<int> Register(PreContractFixedCommissionRange preContractFixedCommissionRange)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();

				try
				{
					var parameters = new DynamicParameters();

					parameters.Add("@poi_contract_fixed_com_range_id", preContractFixedCommissionRange.contract_fixed_com_range_id, DbType.Int32, ParameterDirection.InputOutput);
					parameters.Add("@pii_contract_id", preContractFixedCommissionRange.contract_id, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pii_contract_version", preContractFixedCommissionRange.contract_version, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pii_contract_modification", preContractFixedCommissionRange.contract_modification, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pii_validity_time", preContractFixedCommissionRange.validity_time, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pid_amount", preContractFixedCommissionRange.amount, DbType.Decimal, ParameterDirection.Input);
					parameters.Add("@pid_validity_start_date", preContractFixedCommissionRange.validity_start_date, DbType.DateTime, ParameterDirection.Input);
					parameters.Add("@pid_validity_end_date", preContractFixedCommissionRange.validity_end_date, DbType.DateTime, ParameterDirection.Input);
					parameters.Add("@pib_validity_active", preContractFixedCommissionRange.validity_active, DbType.Boolean, ParameterDirection.Input);
					parameters.Add("@pii_state", preContractFixedCommissionRange.state, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pii_grade", preContractFixedCommissionRange.grade, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pii_register_user_id", preContractFixedCommissionRange.register_user_id, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@piv_register_user_fullname", preContractFixedCommissionRange.register_user_fullname, DbType.String, ParameterDirection.Input);
					parameters.Add("@pid_register_datetime", preContractFixedCommissionRange.register_datetime, DbType.DateTime, ParameterDirection.Input);
					parameters.Add("@pii_update_user_id", preContractFixedCommissionRange.update_user_id, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@piv_update_user_fullname", preContractFixedCommissionRange.update_user_fullname, DbType.String, ParameterDirection.Input);
					parameters.Add("@pid_update_datetime", preContractFixedCommissionRange.update_datetime, DbType.DateTime, ParameterDirection.Input);

					var result = await connection.ExecuteAsync(@"CONTRACT.adv_t_pre_contract_fixed_commission_range_insert_update", parameters, commandType: CommandType.StoredProcedure);

					preContractFixedCommissionRange.contract_fixed_com_range_id = parameters.Get<int>("@poi_contract_fixed_com_range_id");

					return preContractFixedCommissionRange.contract_fixed_com_range_id;
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}
			}
		}

		public async Task<int> RegisterJson(List<PreContractFixedCommissionRange> preContractFixedCommissionRanges, SqlTransaction transaction)
		{
			var parameters = new DynamicParameters();

			parameters.Add("jsonData", JsonConvert.SerializeObject(preContractFixedCommissionRanges), dbType: DbType.String);
			var result = await transaction.Connection.ExecuteAsync("contract.adv_t_pre_contract_fixed_commission_range_insert_update_json", parameters, transaction, commandType: CommandType.StoredProcedure);

			return result;
		}
	}
}
