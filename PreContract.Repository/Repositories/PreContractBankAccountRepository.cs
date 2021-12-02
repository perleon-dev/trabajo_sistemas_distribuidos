using Contracts.Api.Domain.Aggregates.PreContractBankAccountAggregate;
using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Contracts.Api.Repository.Repositories
{
	public class PreContractBankAccountRepository : IPreContractBankAccountRepository
	{
		readonly string _connectionString = string.Empty;

		public PreContractBankAccountRepository(){}

		public PreContractBankAccountRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		public async Task<int> Register(PreContractBankAccount preContractBankAccount)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();

				try
				{
					var parameters = GetParameters(preContractBankAccount);
					
					var result = await connection.ExecuteAsync(@"CONTRACT.adv_t_pre_contract_bank_account_insert_update", parameters, commandType: CommandType.StoredProcedure);

					preContractBankAccount.contract_bank_account_id = parameters.Get<int>("@poi_contract_bank_account_id");

					return preContractBankAccount.contract_bank_account_id;
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}
			}
		}

		public async Task<int> Register(PreContractBankAccount preContractBankAccount, SqlTransaction transaction)
		{
			var parameters = GetParameters(preContractBankAccount);
			
			var result = await transaction.Connection.ExecuteAsync(@"CONTRACT.adv_t_pre_contract_bank_account_insert_update", parameters, transaction: transaction, commandType: CommandType.StoredProcedure);

			preContractBankAccount.contract_bank_account_id = parameters.Get<int>("@poi_contract_bank_account_id");

			return preContractBankAccount.contract_bank_account_id;
		}

		private DynamicParameters GetParameters(PreContractBankAccount preContractBankAccount)
		{
			var parameters = new DynamicParameters();
			
			parameters.Add("@poi_contract_bank_account_id", preContractBankAccount.contract_bank_account_id, DbType.Int32, ParameterDirection.InputOutput);
			parameters.Add("@pii_contract_id", preContractBankAccount.contract_id, DbType.Int32, ParameterDirection.Input);
			parameters.Add("@pii_contract_version", preContractBankAccount.contract_version, DbType.Int32, ParameterDirection.Input);
			parameters.Add("@pii_contract_modification", preContractBankAccount.contract_modification, DbType.Int32, ParameterDirection.Input);
			parameters.Add("@pii_bank_id", preContractBankAccount.bank_id, DbType.Int32, ParameterDirection.Input);
			parameters.Add("@pii_currency_id", preContractBankAccount.currency_id, DbType.Int32, ParameterDirection.Input);
			parameters.Add("@piv_account_number", preContractBankAccount.account_number, DbType.String, ParameterDirection.Input);
			parameters.Add("@piv_cci_account_number", preContractBankAccount.cci_account_number, DbType.String, ParameterDirection.Input);
			parameters.Add("@pii_type_account", preContractBankAccount.type_account, DbType.Int32, ParameterDirection.Input);
			parameters.Add("@pii_register_user_id", preContractBankAccount.register_user_id, DbType.Int32, ParameterDirection.Input);
			parameters.Add("@piv_register_user_fullname", preContractBankAccount.register_user_fullname, DbType.String, ParameterDirection.Input);
			parameters.Add("@pid_register_datetime", preContractBankAccount.register_datetime, DbType.DateTime, ParameterDirection.Input);
			parameters.Add("@pii_update_user_id", preContractBankAccount.update_user_id, DbType.Int32, ParameterDirection.Input);
			parameters.Add("@piv_update_user_fullname", preContractBankAccount.update_user_fullname, DbType.String, ParameterDirection.Input);
			parameters.Add("@pid_update_datetime", preContractBankAccount.update_datetime, DbType.DateTime, ParameterDirection.Input);
			parameters.Add("@pii_state", preContractBankAccount.state, DbType.Int32, ParameterDirection.Input);

			return parameters;
		}
	}
}
