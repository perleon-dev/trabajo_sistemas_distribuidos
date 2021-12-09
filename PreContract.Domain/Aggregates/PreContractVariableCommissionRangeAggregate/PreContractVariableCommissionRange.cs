using PreContracts.Api.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreContracts.Api.Domain.Aggregates.PreContractVariableCommissionRangeAggregate
{
	public class PreContractVariableCommissionRange : Entity
	{
		public int contract_variable_com_range_id { get; set; }
		public int contract_id { get; set; }
		public int contract_version { get; set; }
		public int contract_modification { get; set; }
		public int contract_tradename_id { get; }
		public int state { get; }
		public int validity_time { get; }
		public bool? validity_active { get; }
		public decimal percentage { get; }
		public DateTime? validity_start_date { get; }
		public DateTime? validity_end_date { get; }
		public int grade { get; }
		public int? register_user_id { get; }
		public string register_user_fullname { get; }
		public DateTime? register_datetime { get; }
		public int? update_user_id { get; }
		public string update_user_fullname { get; }
		public DateTime? update_datetime { get; }
		public int? category_id { get; }

		public PreContractVariableCommissionRange()
		{
		}

		public PreContractVariableCommissionRange(int contract_id, int contract_version, int contract_modification, int contract_tradename_id, int state, int validity_time, bool? validity_active, decimal percentage, DateTime? validity_start_date, DateTime? validity_end_date, int grade, int? register_user_id, string register_user_fullname, DateTime? register_datetime, int? update_user_id, string update_user_fullname, DateTime? update_datetime, int? category_id)
		{
			this.contract_id = contract_id;
			this.contract_version = contract_version;
			this.contract_modification = contract_modification;
			this.contract_tradename_id = contract_tradename_id;
			this.state = state;
			this.validity_time = validity_time;
			this.validity_active = validity_active;
			this.percentage = percentage;
			this.validity_start_date = validity_start_date;
			this.validity_end_date = validity_end_date;
			this.grade = grade;
			this.register_user_id = register_user_id;
			this.register_user_fullname = register_user_fullname;
			this.register_datetime = register_datetime;
			this.update_user_id = update_user_id;
			this.update_user_fullname = update_user_fullname;
			this.update_datetime = update_datetime;
			this.category_id = category_id;
		}

		public PreContractVariableCommissionRange(int contract_variable_com_range_id, int contract_id, int contract_version, int contract_modification, int contract_tradename_id, int state, int validity_time, bool? validity_active, decimal percentage, DateTime? validity_start_date, DateTime? validity_end_date, int grade, int? register_user_id, string register_user_fullname, DateTime? register_datetime, int? update_user_id, string update_user_fullname, DateTime? update_datetime, int? category_id)
		{
			this.contract_variable_com_range_id = contract_variable_com_range_id;
			this.contract_id = contract_id;
			this.contract_version = contract_version;
			this.contract_modification = contract_modification;
			this.contract_tradename_id = contract_tradename_id;
			this.state = state;
			this.validity_time = validity_time;
			this.validity_active = validity_active;
			this.percentage = percentage;
			this.validity_start_date = validity_start_date;
			this.validity_end_date = validity_end_date;
			this.grade = grade;
			this.register_user_id = register_user_id;
			this.register_user_fullname = register_user_fullname;
			this.register_datetime = register_datetime;
			this.update_user_id = update_user_id;
			this.update_user_fullname = update_user_fullname;
			this.update_datetime = update_datetime;
			this.category_id = category_id;
		}
	}
}
