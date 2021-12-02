using Contracts.Api.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Api.Domain.Aggregates.PreContractFixedCommissionRangeAggregate
{
	public class PreContractFixedCommissionRange : Entity
	{
		public int contract_fixed_com_range_id { get; set; }
		public int contract_id { get; set; }
		public int contract_version { get; set; }
		public int contract_modification { get; set; }
		public int validity_time { get; }
		public decimal amount { get; }
		public DateTime? validity_start_date { get; }
		public DateTime? validity_end_date { get; }
		public bool? validity_active { get; }
		public int state { get; }
		public int grade { get; }
		public int register_user_id { get; }
		public string register_user_fullname { get; }
		public DateTime register_datetime { get; }
		public int? update_user_id { get; }
		public string update_user_fullname { get; }
		public DateTime? update_datetime { get; }

		public PreContractFixedCommissionRange()
		{
		}

		public PreContractFixedCommissionRange(int contract_id, int contract_version, int contract_modification, int validity_time, decimal amount, DateTime? validity_start_date, DateTime? validity_end_date, bool? validity_active, int state, int grade, int register_user_id, string register_user_fullname, DateTime register_datetime, int? update_user_id, string update_user_fullname, DateTime? update_datetime)
		{
			this.contract_id = contract_id;
			this.contract_version = contract_version;
			this.contract_modification = contract_modification;
			this.validity_time = validity_time;
			this.amount = amount;
			this.validity_start_date = validity_start_date;
			this.validity_end_date = validity_end_date;
			this.validity_active = validity_active;
			this.state = state;
			this.grade = grade;
			this.register_user_id = register_user_id;
			this.register_user_fullname = register_user_fullname;
			this.register_datetime = register_datetime;
			this.update_user_id = update_user_id;
			this.update_user_fullname = update_user_fullname;
			this.update_datetime = update_datetime;
		}

		public PreContractFixedCommissionRange(int contract_fixed_com_range_id, int contract_id, int contract_version, int contract_modification, int validity_time, decimal amount, DateTime? validity_start_date, DateTime? validity_end_date, bool? validity_active, int state, int grade, int register_user_id, string register_user_fullname, DateTime register_datetime, int? update_user_id, string update_user_fullname, DateTime? update_datetime)
		{
			this.contract_fixed_com_range_id = contract_fixed_com_range_id;
			this.contract_id = contract_id;
			this.contract_version = contract_version;
			this.contract_modification = contract_modification;
			this.validity_time = validity_time;
			this.amount = amount;
			this.validity_start_date = validity_start_date;
			this.validity_end_date = validity_end_date;
			this.validity_active = validity_active;
			this.state = state;
			this.grade = grade;
			this.register_user_id = register_user_id;
			this.register_user_fullname = register_user_fullname;
			this.register_datetime = register_datetime;
			this.update_user_id = update_user_id;
			this.update_user_fullname = update_user_fullname;
			this.update_datetime = update_datetime;
		}
	}
}
