using PreContracts.Api.Domain.Core;
using System;

namespace PreContracts.Api.Domain.Aggregates.PreContractBankAccountAggregate
{
	public class PreContractBankAccount : Entity
	{
		public int contract_bank_account_id { get; set; }
		public int? contract_id { get; set; }
		public int? contract_version { get; set; }
		public int? contract_modification { get; set; }
		public int? bank_id { get; }
		public int? currency_id { get; }
		public string account_number { get; }
		public string cci_account_number { get; }
		public int? type_account { get; }
        public int? state { get; set; }
        public int? register_user_id { get; }
		public string register_user_fullname { get; }
		public DateTime? register_datetime { get; }
		public int? update_user_id { get; }
		public string update_user_fullname { get; }
		public DateTime? update_datetime { get; }

		public PreContractBankAccount()
		{
		}

		public PreContractBankAccount(int? contract_id, int? contract_version, int? contract_modification, int? bank_id, int? currency_id, string account_number, string cci_account_number, int? type_account, int? register_user_id, string register_user_fullname, DateTime? register_datetime, int? update_user_id, string update_user_fullname, DateTime? update_datetime, int? state)
		{
			this.contract_id = contract_id;
			this.contract_version = contract_version;
			this.contract_modification = contract_modification;
			this.bank_id = bank_id;
			this.currency_id = currency_id;
			this.account_number = account_number;
			this.cci_account_number = cci_account_number;
			this.type_account = type_account;
			this.register_user_id = register_user_id;
			this.register_user_fullname = register_user_fullname;
			this.register_datetime = register_datetime;
			this.update_user_id = update_user_id;
			this.update_user_fullname = update_user_fullname;
			this.update_datetime = update_datetime;
			this.state = state;
		}

		public PreContractBankAccount(int contract_bank_account_id, int? contract_id, int? contract_version, int? contract_modification, int? bank_id, int? currency_id, string account_number, string cci_account_number, int? type_account, int? register_user_id, string register_user_fullname, DateTime? register_datetime, int? update_user_id, string update_user_fullname, DateTime? update_datetime, int? state)
		{
			this.contract_bank_account_id = contract_bank_account_id;
			this.contract_id = contract_id;
			this.contract_version = contract_version;
			this.contract_modification = contract_modification;
			this.bank_id = bank_id;
			this.currency_id = currency_id;
			this.account_number = account_number;
			this.cci_account_number = cci_account_number;
			this.type_account = type_account;
			this.register_user_id = register_user_id;
			this.register_user_fullname = register_user_fullname;
			this.register_datetime = register_datetime;
			this.update_user_id = update_user_id;
			this.update_user_fullname = update_user_fullname;
			this.update_datetime = update_datetime;
			this.state = state;
		}
	}
}
