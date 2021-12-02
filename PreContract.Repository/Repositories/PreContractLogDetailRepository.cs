using Contracts.Api.Domain.Aggregates.PreContractLogDetailAggregate;
using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Api.Repository.Repositories
{
	public class PreContractLogDetailRepository : IPreContractLogDetailRepository
	{
		readonly string _connectionString = string.Empty;

		public PreContractLogDetailRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		public PreContractLogDetailRepository()
		{

		}

		public async Task<int> Register(PreContractLogDetail preContractLogDetail)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();

				try
				{
					var parameters = new DynamicParameters();

					parameters.Add("@poi_log_detail_id", preContractLogDetail.log_detail_id, DbType.Int32, ParameterDirection.InputOutput);
					parameters.Add("@piv_document_id", preContractLogDetail.document_id, DbType.String, ParameterDirection.Input);
					parameters.Add("@piv_business_name", preContractLogDetail.business_name, DbType.String, ParameterDirection.Input);
					parameters.Add("@piv_id_summa", preContractLogDetail.id_summa, DbType.String, ParameterDirection.Input);
					parameters.Add("@piv_item", preContractLogDetail.item, DbType.String, ParameterDirection.Input);
					parameters.Add("@piv_segment", preContractLogDetail.segment, DbType.String, ParameterDirection.Input);
					parameters.Add("@pii_commission_variable", preContractLogDetail.commission_variable, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pii_category_id", preContractLogDetail.category_id, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@piv_category_name", preContractLogDetail.category_name, DbType.String, ParameterDirection.Input);
					parameters.Add("@piv_validity", preContractLogDetail.validity, DbType.String, ParameterDirection.Input);
					parameters.Add("@piv_commisison_type", preContractLogDetail.commisison_type, DbType.String, ParameterDirection.Input);
					parameters.Add("@pii_month_range_commission_variable", preContractLogDetail.month_range_commission_variable, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pid_percentage_commission_variable", preContractLogDetail.percentage_commission_variable, DbType.Decimal, ParameterDirection.Input);
					parameters.Add("@pii_fixed_commission", preContractLogDetail.fixed_commission, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pii_month_range_fixed_commission", preContractLogDetail.month_range_fixed_commission, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pid_fixed_commisison_amount", preContractLogDetail.fixed_commisison_amount, DbType.Decimal, ParameterDirection.Input);
					parameters.Add("@piv_start_date_contract", preContractLogDetail.start_date_contract, DbType.String, ParameterDirection.Input);
					parameters.Add("@piv_bank_name", preContractLogDetail.bank_name, DbType.String, ParameterDirection.Input);
					parameters.Add("@piv_bank_account", preContractLogDetail.bank_account, DbType.String, ParameterDirection.Input);
					parameters.Add("@piv_interbank_account", preContractLogDetail.interbank_account, DbType.String, ParameterDirection.Input);
					parameters.Add("@piv_currency_name", preContractLogDetail.currency_name, DbType.String, ParameterDirection.Input);
					parameters.Add("@piv_bank_account_type_name", preContractLogDetail.bank_account_type_name, DbType.String, ParameterDirection.Input);
					parameters.Add("@piv_observation", preContractLogDetail.observation, DbType.String, ParameterDirection.Input);
					parameters.Add("@pii_state", preContractLogDetail.state, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pii_log_id", preContractLogDetail.log_id, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pii_register_user_id", preContractLogDetail.register_user_id, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@piv_register_user_fullname", preContractLogDetail.register_user_fullname, DbType.String, ParameterDirection.Input);
					parameters.Add("@pid_register_datetime", preContractLogDetail.register_datetime, DbType.DateTime, ParameterDirection.Input);

					var result = await connection.ExecuteAsync(@"CONTRACT.adv_t_pre_contract_log_detail_insert_update", parameters, commandType: CommandType.StoredProcedure);

					preContractLogDetail.log_detail_id = parameters.Get<int>("@poi_log_detail_id");

					return preContractLogDetail.log_detail_id;
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}
			}
		}

		public async Task<int> RegisterAsyncJson(List<PreContractLogDetail> preContractLogDetails, SqlConnection connection, SqlTransaction transaction)
		{
			var parameters = new DynamicParameters();
			parameters.Add("@jsonData", JsonConvert.SerializeObject(preContractLogDetails, new IsoDateTimeConverter() { DateTimeFormat = "dd-MM-yyyy HH:mm:ss" }), DbType.String);
			return await connection.ExecuteAsync("CONTRACT.adv_t_pre_contract_log_detail_insert_json", parameters, transaction, commandType: CommandType.StoredProcedure);
		}

        public async Task<int> UpdateState(PreContractLogDetail preContractLogDetail)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
				await connection.OpenAsync();
                try
                {
					var parameters = new DynamicParameters();

					parameters.Add("@pii_log_detail_id", preContractLogDetail.log_detail_id, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@piv_observation", preContractLogDetail.observation, DbType.String, ParameterDirection.Input);
					parameters.Add("@pii_state", preContractLogDetail.state, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@pii_register_user_id", preContractLogDetail.register_user_id, DbType.Int32, ParameterDirection.Input);
					parameters.Add("@piv_register_user_fullname", preContractLogDetail.register_user_fullname, DbType.String, ParameterDirection.Input);
					parameters.Add("@pid_register_datetime", preContractLogDetail.register_datetime, DbType.DateTime, ParameterDirection.Input);

					return await connection.ExecuteAsync(@"CONTRACT.adv_t_pre_contract_log_detail_insert_update", parameters, commandType: CommandType.StoredProcedure);
				}
                catch (Exception ex)
                {
					throw new Exception(ex.Message);
				}
            }
        }

		public async Task<int> RegisterAsyncJson(List<PreContractLogDetail> preContractLogDetails)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();

				try
				{
					var parameters = new DynamicParameters();

					parameters.Add("@jsonData", JsonConvert.SerializeObject(preContractLogDetails, new IsoDateTimeConverter() { DateTimeFormat = "dd-MM-yyyy HH:mm:ss" }), DbType.String);

					return await connection.ExecuteAsync(@"CONTRACT.adv_t_pre_contract_log_detail_insert_json", parameters, commandType: CommandType.StoredProcedure);
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}
			}
		}
	}
}
