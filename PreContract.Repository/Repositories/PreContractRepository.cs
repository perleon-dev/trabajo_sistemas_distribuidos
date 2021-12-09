using PreContracts.Api.Domain.Aggregates.PreContractAggregate;
using PreContracts.Api.Domain.Aggregates.PreContractBankAccountAggregate;
using PreContracts.Api.Domain.Aggregates.PreContractEconomicConditionAggregate;
using PreContracts.Api.Domain.Aggregates.PreContractFixedCommissionRangeAggregate;
using PreContracts.Api.Domain.Aggregates.PreContractTradenameAggregate;
using PreContracts.Api.Domain.Aggregates.PreContractVariableCommissionRangeAggregate;
using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PreContracts.Api.Repository.Repositories
{
	public class PreContractRepository : IPreContractRepository
	{
		readonly string _connectionString = string.Empty;

		public PreContractRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		public async Task<int> Register(PreContract preContract)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();

				try
				{
					var parameters = GetParameters(preContract);

					var result = await connection.ExecuteAsync(@"CONTRACT.adv_t_pre_contract_insert_update", parameters, commandType: CommandType.StoredProcedure);

					preContract.contract_id = parameters.Get<int>("@poi_contract_id");

					return preContract.contract_id;
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}
			}
		}

		private DynamicParameters GetParameters(PreContract preContract)
		{
			var parameters = new DynamicParameters();

			parameters.Add("@poi_contract_id", preContract.contract_id, DbType.Int32, direction: ParameterDirection.InputOutput);
			parameters.Add("@poi_contract_version", preContract.contract_version, DbType.Int32, direction: ParameterDirection.InputOutput);
			parameters.Add("@poi_contract_modification", preContract.contract_modification, DbType.Int32, direction: ParameterDirection.InputOutput);
			parameters.Add("@pid_contract_start_date", preContract.contract_start_date, DbType.DateTime, ParameterDirection.Input);
			parameters.Add("@pid_contract_end_date", preContract.contract_end_date, DbType.DateTime, ParameterDirection.Input);
			parameters.Add("@piv_ruc", preContract.ruc, DbType.String, ParameterDirection.Input);
			parameters.Add("@piv_bank_account", preContract.bank_account, DbType.String, ParameterDirection.Input);
			parameters.Add("@piv_cci", preContract.cci, DbType.String, ParameterDirection.Input);
			parameters.Add("@piv_code_contract", preContract.code_contract, DbType.String, ParameterDirection.Input);
			parameters.Add("@pii_type_seller", preContract.type_seller, DbType.Int32, ParameterDirection.Input);
			parameters.Add("@piv_distribution_type", preContract.distribution_type, DbType.String, ParameterDirection.Input);
			parameters.Add("@pid_product_commission", preContract.product_commission, DbType.Decimal, ParameterDirection.Input);
			parameters.Add("@pii_state", preContract.state, DbType.Int32, ParameterDirection.Input);
			parameters.Add("@pib_active", preContract.active, DbType.Boolean, ParameterDirection.Input);
			parameters.Add("@pii_tradename_id", preContract.tradename_id, DbType.Int32, ParameterDirection.Input);
			parameters.Add("@pii_mall_id", preContract.mall_id, DbType.Int32, ParameterDirection.Input);
			parameters.Add("@piv_bank_id", preContract.bank_id, DbType.String, ParameterDirection.Input);
			parameters.Add("@pii_type_currency", preContract.type_currency, DbType.Int32, ParameterDirection.Input);
			parameters.Add("@piv_bank_account_type", preContract.bank_account_type, DbType.String, ParameterDirection.Input);
			parameters.Add("@pii_segment_id", preContract.segment_id, DbType.Int32, ParameterDirection.Input);
			parameters.Add("@pii_origin", preContract.origin, DbType.Int32, ParameterDirection.Input);
			parameters.Add("@piv_ubigeo", preContract.ubigeo, DbType.String, ParameterDirection.Input);
			parameters.Add("@pii_commercial_template_id", preContract.commercial_template_id, DbType.Int32, ParameterDirection.Input);
			parameters.Add("@pii_register_user_id", preContract.register_user_id, DbType.Int32, ParameterDirection.Input);
			parameters.Add("@piv_register_user_fullname", preContract.register_user_fullname, DbType.String, ParameterDirection.Input);
			parameters.Add("@pid_register_datetime", preContract.register_datetime, DbType.DateTime, ParameterDirection.Input);
			parameters.Add("@pii_update_user_id", preContract.update_user_id, DbType.Int32, ParameterDirection.Input);
			parameters.Add("@piv_update_user_fullname", preContract.update_user_fullname, DbType.String, ParameterDirection.Input);
			parameters.Add("@pid_update_datetime", preContract.update_datetime, DbType.DateTime, ParameterDirection.Input);

			return parameters;
		}

		public async Task<int> RegisterUnified(PreContract preContract, PreContractBankAccount bankAccount, List<PreContractTradename> tradenames, List<PreContractFixedCommissionRange> fixedCommissions, List<PreContractVariableCommissionRange> variableCommissions, List<PreContractEconomicCondition> economicConditions)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();

				using (var transaction = connection.BeginTransaction())
				{
					try
					{
						var parameters = GetParameters(preContract);

						var result = await transaction.Connection.ExecuteAsync(@"CONTRACT.adv_t_pre_contract_insert_update", parameters, transaction, commandType: CommandType.StoredProcedure);
						preContract.contract_id = parameters.Get<int>("@poi_contract_id");
						preContract.contract_version = parameters.Get<int>("@poi_contract_version");
						preContract.contract_modification = parameters.Get<int>("@poi_contract_modification");

						if (bankAccount != null)
						{
							bankAccount.contract_id = preContract.contract_id;
							bankAccount.contract_version = preContract.contract_version;
							bankAccount.contract_modification = preContract.contract_modification;

							await new PreContractBankAccountRepository().Register(bankAccount, transaction);
						}

						tradenames.ForEach(x =>
						{
							x.contract_id = preContract.contract_id;
							x.contract_version = preContract.contract_version;
							x.contract_modification = preContract.contract_modification;
						}
						);
						await new PreContractTradenameRepository().RegisterJson(tradenames, transaction);

						fixedCommissions.ForEach(x =>
						{
							x.contract_id = preContract.contract_id;
							x.contract_version = preContract.contract_version;
							x.contract_modification = preContract.contract_modification;
						}
						);
						await new PreContractFixedCommissionRangeRepository().RegisterJson(fixedCommissions, transaction);

						variableCommissions.ForEach(x =>
						{
							x.contract_id = preContract.contract_id;
							x.contract_version = preContract.contract_version;
							x.contract_modification = preContract.contract_modification;
						}
						);
						await new PreContractVariableCommissionRangeRepository().RegisterJson(variableCommissions, transaction);

						economicConditions.ForEach(x =>
						{
							x.contract_id = preContract.contract_id;
							x.contract_version = preContract.contract_version;
							x.contract_modification = preContract.contract_modification;
						}
						);
						await new PreContractEconomicConditionRepository().RegisterJson(economicConditions, transaction);

						transaction.Commit();

						return preContract.contract_id;
					}
					catch (Exception ex)
					{
						throw new Exception(ex.Message);
					}
				}
			}
		}

		public async Task<int> UpdateStateJson(List<PreContract> preContractList, List<PreContractTradename> tradenameList)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();

				using (var transaction = connection.BeginTransaction())
				{
					try
					{
						var parameters = new DynamicParameters();
						parameters.Add("@jsonData", JsonConvert.SerializeObject(preContractList), DbType.String);
						await connection.ExecuteAsync(@"CONTRACT.adv_t_pre_contract_update_state_json", parameters, transaction, commandType: CommandType.StoredProcedure);

						transaction.Commit();

						return 1;
					}
					catch (Exception ex)
					{
						throw new Exception(ex.Message);
					}
				}
			}
		}
	}
}
