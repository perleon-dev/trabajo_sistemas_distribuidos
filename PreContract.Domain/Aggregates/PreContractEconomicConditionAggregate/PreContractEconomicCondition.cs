using Contracts.Api.Domain.Core;
using System;

namespace Contracts.Api.Domain.Aggregates.PreContractEconomicConditionAggregate
{
	public class PreContractEconomicCondition : Entity
	{
		public int economic_condition_id { get; set; }
		public decimal? commission { get; }
		public int? category_id { get; }
		public int? state { get; }
		public int contract_id { get; set; }
		public int contract_version { get; set; }
		public int contract_modification { get; set; }
		public int? register_user_id { get; }
		public string register_user_fullname { get; }
		public DateTime? register_datetime { get; }
		public int? update_user_id { get; }
		public string update_user_fullname { get; }
		public DateTime? update_datetime { get; }

		public PreContractEconomicCondition()
		{
		}

		public PreContractEconomicCondition(decimal? commission, int? category_id, int? state, int contract_id, int contract_version, int contract_modification, int? register_user_id, string register_user_fullname, DateTime? register_datetime, int? update_user_id, string update_user_fullname, DateTime? update_datetime)
		{
			this.commission = commission;
			this.category_id = category_id;
			this.state = state;
			this.contract_id = contract_id;
			this.contract_version = contract_version;
			this.contract_modification = contract_modification;
			this.register_user_id = register_user_id;
			this.register_user_fullname = register_user_fullname;
			this.register_datetime = register_datetime;
			this.update_user_id = update_user_id;
			this.update_user_fullname = update_user_fullname;
			this.update_datetime = update_datetime;
		}

		public PreContractEconomicCondition(int economic_condition_id, decimal? commission, int? category_id, int? state, int contract_id, int contract_version, int contract_modification, int? register_user_id, string register_user_fullname, DateTime? register_datetime, int? update_user_id, string update_user_fullname, DateTime? update_datetime)
		{
			this.economic_condition_id = economic_condition_id;
			this.commission = commission;
			this.category_id = category_id;
			this.state = state;
			this.contract_id = contract_id;
			this.contract_version = contract_version;
			this.contract_modification = contract_modification;
			this.register_user_id = register_user_id;
			this.register_user_fullname = register_user_fullname;
			this.register_datetime = register_datetime;
			this.update_user_id = update_user_id;
			this.update_user_fullname = update_user_fullname;
			this.update_datetime = update_datetime;
		}
	}
}
