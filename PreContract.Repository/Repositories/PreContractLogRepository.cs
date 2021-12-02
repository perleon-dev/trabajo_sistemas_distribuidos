using Contracts.Api.Domain.Aggregates.PreContractLogAggregate;
using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Contracts.Api.Repository.Repositories
{
	public class PreContractLogRepository : IPreContractLogRepository
	{
		readonly string _connectionString = string.Empty;

		public PreContractLogRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		public async Task<int> Register(PreContractLog preContractLog)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();
                using (SqlTransaction transaction = connection.BeginTransaction())
                { 
					try
					{
						var parameters = new DynamicParameters();

						parameters.Add("@poi_log_id", preContractLog.log_id, DbType.Int32, ParameterDirection.InputOutput);
						parameters.Add("@piv_file_name", preContractLog.file_name, DbType.String, ParameterDirection.Input);
						parameters.Add("@pii_number_record", preContractLog.number_record, DbType.Int32, ParameterDirection.Input);
						parameters.Add("@pii_contract_type", preContractLog.contract_type, DbType.Int32, ParameterDirection.Input);
						parameters.Add("@pii_state", preContractLog.state, DbType.Int32, ParameterDirection.Input);
						parameters.Add("@pii_register_user_id", preContractLog.register_user_id, DbType.Int32, ParameterDirection.Input);
						parameters.Add("@piv_register_user_fullname", preContractLog.register_user_fullname, DbType.String, ParameterDirection.Input);
						parameters.Add("@pid_register_datetime", preContractLog.register_datetime, DbType.DateTime, ParameterDirection.Input);

						var result = await transaction.Connection.ExecuteAsync(@"CONTRACT.adv_t_pre_contract_log_insert_update", parameters, transaction, commandType: CommandType.StoredProcedure);

						preContractLog.log_id = parameters.Get<int>("@poi_log_id");

						if(preContractLog.preContractLogDetailList!=null && preContractLog.preContractLogDetailList.Count > 0)
						{
							preContractLog.SetLogId();
							await new PreContractLogDetailRepository().RegisterAsyncJson(preContractLog.preContractLogDetailList, connection, transaction);
						}

						transaction.Commit();
						return preContractLog.log_id;
					}
					catch (Exception ex)
					{
						transaction.Rollback();
						throw new Exception(ex.Message);
					}
				}				
			}
		}

        public async Task<int> UpdateStatus(PreContractLog preContractLog)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
				await connection.OpenAsync();
                try
                {
					var parameters = new DynamicParameters();

					parameters.Add("@pii_log_id", preContractLog.log_id, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pii_state", preContractLog.state, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pii_register_user_id", preContractLog.register_user_id, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@piv_register_user_fullname", preContractLog.register_user_fullname, DbType.String, ParameterDirection.Input);
					parameters.Add("@pid_register_datetime", preContractLog.register_datetime, DbType.DateTime, ParameterDirection.Input);

					return await connection.ExecuteAsync(@"CONTRACT.adv_t_pre_contract_log_update_state", parameters, commandType: CommandType.StoredProcedure);

				}
				catch (Exception ex)
                {
					throw new Exception(ex.Message);
				}
            }
        }
    }
}
