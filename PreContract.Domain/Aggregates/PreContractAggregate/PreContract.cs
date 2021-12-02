using Contracts.Api.Domain.Core;
using System;

namespace Contracts.Api.Domain.Aggregates.PreContractAggregate
{
	public class PreContract : Entity
	{
		public int contract_id { get; set; }
		public int contract_version { get; set; }
		public int contract_modification { get; set; }
		public DateTime? contract_start_date { get; }
		public DateTime? contract_end_date { get; }
		public string ruc { get; }
		public string bank_account { get; }
		public string cci { get; }
		public string code_contract { get; }
		public int? type_seller { get; }
		public string distribution_type { get; }
		public decimal? product_commission { get; }
		public int? state { get; }
		public bool? active { get; }
		public int? tradename_id { get; }
		public int? mall_id { get; }
		public string bank_id { get; }
		public int? type_currency { get; }
		public string bank_account_type { get; }
		public int? segment_id { get; }
		public int? origin { get; }
		public string ubigeo { get; }
		public int? commercial_template_id { get; }
		public int? register_user_id { get; }
		public string register_user_fullname { get; }
		public DateTime? register_datetime { get; }
		public int? update_user_id { get; }
		public string update_user_fullname { get; }
		public DateTime? update_datetime { get; }

		public PreContract()
		{
		}

		public PreContract(int contract_version, int contract_modification, DateTime? contract_start_date, DateTime? contract_end_date, string ruc, string bank_account, string cci, string code_contract, int? type_seller, string distribution_type, decimal? product_commission, int? state, bool? active, int? tradename_id, int? mall_id, string bank_id, int? type_currency, string bank_account_type, int? segment_id, int? origin, string ubigeo, int? commercial_template_id, int? register_user_id, string register_user_fullname, DateTime? register_datetime, int? update_user_id, string update_user_fullname, DateTime? update_datetime)
		{
			this.contract_version = contract_version;
			this.contract_modification = contract_modification;
			this.contract_start_date = contract_start_date;
			this.contract_end_date = contract_end_date;
			this.ruc = ruc;
			this.bank_account = bank_account;
			this.cci = cci;
			this.code_contract = code_contract;
			this.type_seller = type_seller;
			this.distribution_type = distribution_type;
			this.product_commission = product_commission;
			this.state = state;
			this.active = active;
			this.tradename_id = tradename_id;
			this.mall_id = mall_id;
			this.bank_id = bank_id;
			this.type_currency = type_currency;
			this.bank_account_type = bank_account_type;
			this.segment_id = segment_id;
			this.origin = origin;
			this.ubigeo = ubigeo;
			this.commercial_template_id = commercial_template_id;
			this.register_user_id = register_user_id;
			this.register_user_fullname = register_user_fullname;
			this.register_datetime = register_datetime;
			this.update_user_id = update_user_id;
			this.update_user_fullname = update_user_fullname;
			this.update_datetime = update_datetime;
		}

		public PreContract(int contract_id, int contract_version, int contract_modification, DateTime? contract_start_date, DateTime? contract_end_date, string ruc, string bank_account, string cci, string code_contract, int? type_seller, string distribution_type, decimal? product_commission, int? state, bool? active, int? tradename_id, int? mall_id, string bank_id, int? type_currency, string bank_account_type, int? segment_id, int? origin, string ubigeo, int? commercial_template_id, int? register_user_id, string register_user_fullname, DateTime? register_datetime, int? update_user_id, string update_user_fullname, DateTime? update_datetime)
		{
			this.contract_id = contract_id;
			this.contract_version = contract_version;
			this.contract_modification = contract_modification;
			this.contract_start_date = contract_start_date;
			this.contract_end_date = contract_end_date;
			this.ruc = ruc;
			this.bank_account = bank_account;
			this.cci = cci;
			this.code_contract = code_contract;
			this.type_seller = type_seller;
			this.distribution_type = distribution_type;
			this.product_commission = product_commission;
			this.state = state;
			this.active = active;
			this.tradename_id = tradename_id;
			this.mall_id = mall_id;
			this.bank_id = bank_id;
			this.type_currency = type_currency;
			this.bank_account_type = bank_account_type;
			this.segment_id = segment_id;
			this.origin = origin;
			this.ubigeo = ubigeo;
			this.commercial_template_id = commercial_template_id;
			this.register_user_id = register_user_id;
			this.register_user_fullname = register_user_fullname;
			this.register_datetime = register_datetime;
			this.update_user_id = update_user_id;
			this.update_user_fullname = update_user_fullname;
			this.update_datetime = update_datetime;
		}

		public PreContract(string ruc, int? state, int? update_user_id, string update_user_fullname)
		{
			this.ruc = ruc;
			this.state = state;
			this.update_user_id = update_user_id;
			this.update_user_fullname = update_user_fullname;
		}

		public PreContract(string ruc, int? type_seller, int? state, int? update_user_id, string update_user_fullname)
		{
			this.ruc = ruc;
			this.type_seller = type_seller;
			this.state = state;
			this.update_user_id = update_user_id;
			this.update_user_fullname = update_user_fullname;
		}

		public PreContract(int contract_id, int contract_version, int contract_modification, int? state, int? update_user_id, string update_user_fullname)
		{
			this.contract_id = contract_id;
			this.contract_version = contract_version;
			this.contract_modification = contract_modification;
			this.state = state;
			this.update_user_id = update_user_id;
			this.update_user_fullname = update_user_fullname;
		}
	}
}
