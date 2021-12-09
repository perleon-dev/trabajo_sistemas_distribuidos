using PreContracts.Api.Domain.Core;
using System;

namespace PreContracts.Api.Domain.Aggregates.PreContractLogDetailAggregate
{
	public class PreContractLogDetail : Entity
	{
		public int log_detail_id { get; set; }
		public string document_id { get; }
		public string business_name { get; }
		public string id_summa { get; }
		public string item { get; }
		public string segment { get; }
		public int? commission_variable { get; }
		public int? category_id { get; }
		public string category_name { get; }
		public string validity { get; } 
		public string commisison_type { get; }
		public int? month_range_commission_variable { get; }
		public decimal? percentage_commission_variable { get; }
		public int? fixed_commission { get; }
		public int? month_range_fixed_commission { get; }
		public decimal? fixed_commisison_amount { get; }
		public string start_date_contract { get; }
		public string bank_name { get; }
		public string bank_account { get; }
		public string interbank_account { get; }
		public string currency_name { get; }
		public string bank_account_type_name { get; }
		public string observation { get; }
		public int? state { get; }
		public int? log_id { get; set; }
		public int? register_user_id { get; }
		public string register_user_fullname { get; }
		public DateTime? register_datetime { get; }

		public PreContractLogDetail()
		{
		}

		public PreContractLogDetail(string document_id, string business_name, string id_summa, string item, string segment, int? commission_variable, int? category_id, string category_name, string validity, string commisison_type, int? month_range_commission_variable, decimal? percentage_commission_variable, int? fixed_commission, int? month_range_fixed_commission, decimal? fixed_commisison_amount, string start_date_contract, string bank_name, string bank_account, string interbank_account, string currency_name, string bank_account_type_name, string observation, int? state, int? log_id, int? register_user_id, string register_user_fullname, DateTime? register_datetime)
		{
			this.document_id = document_id;
			this.business_name = business_name;
			this.id_summa = id_summa;
			this.item = item;
			this.segment = segment;
			this.commission_variable = commission_variable;
			this.category_id = category_id;
			this.category_name = category_name;
			this.validity = validity;
			this.commisison_type = commisison_type;
			this.month_range_commission_variable = month_range_commission_variable;
			this.percentage_commission_variable = percentage_commission_variable;
			this.fixed_commission = fixed_commission;
			this.month_range_fixed_commission = month_range_fixed_commission;
			this.fixed_commisison_amount = fixed_commisison_amount;
			this.start_date_contract = start_date_contract;
			this.bank_name = bank_name;
			this.bank_account = bank_account;
			this.interbank_account = interbank_account;
			this.currency_name = currency_name;
			this.bank_account_type_name = bank_account_type_name;
			this.observation = observation;
			this.state = state;
			this.log_id = log_id;
			this.register_user_id = register_user_id;
			this.register_user_fullname = register_user_fullname;
			this.register_datetime = register_datetime;
		}

		public PreContractLogDetail(int log_detail_id, string document_id, string business_name, string id_summa, string item, string segment, int? commission_variable, int? category_id, string category_name, string validity, string commisison_type, int? month_range_commission_variable, decimal? percentage_commission_variable, int? fixed_commission, int? month_range_fixed_commission, decimal? fixed_commisison_amount, string start_date_contract, string bank_name, string bank_account, string interbank_account, string currency_name, string bank_account_type_name, string observation, int? state, int? log_id, int? register_user_id, string register_user_fullname, DateTime? register_datetime)
		{
			this.log_detail_id = log_detail_id;
			this.document_id = document_id;
			this.business_name = business_name;
			this.id_summa = id_summa;
			this.item = item;
			this.segment = segment;
			this.commission_variable = commission_variable;
			this.category_id = category_id;
			this.category_name = category_name;
			this.validity = validity;
			this.commisison_type = commisison_type;
			this.month_range_commission_variable = month_range_commission_variable;
			this.percentage_commission_variable = percentage_commission_variable;
			this.fixed_commission = fixed_commission;
			this.month_range_fixed_commission = month_range_fixed_commission;
			this.fixed_commisison_amount = fixed_commisison_amount;
			this.start_date_contract = start_date_contract;
			this.bank_name = bank_name;
			this.bank_account = bank_account;
			this.interbank_account = interbank_account;
			this.currency_name = currency_name;
			this.bank_account_type_name = bank_account_type_name;
			this.observation = observation;
			this.state = state;
			this.log_id = log_id;
			this.register_user_id = register_user_id;
			this.register_user_fullname = register_user_fullname;
			this.register_datetime = register_datetime;
		}

        public PreContractLogDetail(string document_id, string id_summa, int? category_id, decimal? fixed_commisison_amount, string start_date_contract, int? state, int? register_user_id, string register_user_fullname, DateTime? register_datetime, string observation)
        {
			this.document_id = document_id;
			this.id_summa = id_summa;
			this.category_id = category_id;
			this.fixed_commisison_amount = fixed_commisison_amount;
			this.start_date_contract = start_date_contract;
			this.state = state;
			this.register_user_id = register_user_id;
			this.register_user_fullname = register_user_fullname;
			this.register_datetime = register_datetime;
			this.observation = observation;
		}

        public PreContractLogDetail(int log_detail_id, string observation, int? state, int? register_user_id, string register_user_fullname, DateTime? register_datetime)
        {
			this.log_detail_id = log_detail_id;
			this.observation = observation;
			this.state = state;
			this.register_user_id = register_user_id;
			this.register_user_fullname = register_user_fullname;
			this.register_datetime = register_datetime;
		}

		public PreContractLogDetail(string document_id, string id_summa, string item, string segment, int commission_variable, int? category_id, string category_name, int? month_range_commission_variable, decimal? percentage_commission_variable, string start_date_contract, string bank_name, string bank_account, string interbank_account, string currency_name, string bank_account_type_name, string observation, int? state, int? register_user_id, string register_user_fullname, DateTime? register_datetime)
		{
			this.document_id = document_id;
			this.id_summa = id_summa;
			this.item = item;
			this.segment = segment;
			this.category_id = category_id;
			this.commission_variable = commission_variable;
			this.category_name = category_name;
			this.month_range_commission_variable = month_range_commission_variable;
			this.percentage_commission_variable = percentage_commission_variable;
			this.start_date_contract = start_date_contract;
			this.bank_name = bank_name;
			this.bank_account = bank_account;
			this.interbank_account = interbank_account;
			this.currency_name = currency_name;
			this.bank_account_type_name = bank_account_type_name;
			this.observation = observation;
			this.state = state;
			this.register_user_id = register_user_id;
			this.register_user_fullname = register_user_fullname;
			this.register_datetime = register_datetime;
		}

	}
}
