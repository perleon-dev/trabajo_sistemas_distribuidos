using PreContracts.Api.Domain.Aggregates.LogContractAggregate;
using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Contracts.Repository
{
	public class LogContractRepository : ILogContractRepository
	{
		readonly string _connectionString = string.Empty;

		public LogContractRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		public async Task<int> Register(LogContract logContract)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();

				try
				{
					var parameters = new DynamicParameters();

					parameters.Add("@poi_log_contract_id", logContract.logContractId, DbType.Int32, ParameterDirection.InputOutput);
					parameters.Add("@pii_type_process_id", logContract.typeProcessId, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pii_file_storage_id", logContract.fileStorageId, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pii_state", logContract.state, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@piv_error_message", logContract.errorMessage, DbType.String, ParameterDirection.Input);					
					parameters.Add("@pii_register_user_id", logContract.registerUserId, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@piv_register_user_fullname", logContract.registerUserFullname, DbType.String, ParameterDirection.Input);
					parameters.Add("@pii_update_user_id", logContract.updateUserId, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@piv_update_user_fullname", logContract.updateUserFullname, DbType.String, ParameterDirection.Input);

					var result = await connection.ExecuteAsync(@"CONTRACT.ADV_T_LOG_CONTRACT_insert_update", parameters, commandType: CommandType.StoredProcedure);

					logContract.logContractId = parameters.Get<int>("@poi_log_contract_id");

					return logContract.logContractId;
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}
			}
		}
	}
}