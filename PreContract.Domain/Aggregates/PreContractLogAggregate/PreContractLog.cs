using Contracts.Api.Domain.Aggregates.PreContractLogDetailAggregate;
using Contracts.Api.Domain.Core;
using System;
using System.Collections.Generic;

namespace Contracts.Api.Domain.Aggregates.PreContractLogAggregate
{
	public class PreContractLog : Entity
	{
		public int log_id { get; set; }
		public string file_name { get; }
		public int? number_record { get; }
		public int? contract_type { get; }
		public int? state { get; }
		public int? register_user_id { get; }
		public string register_user_fullname { get; } 
		public DateTime? register_datetime { get; }
        public List<PreContractLogDetail> preContractLogDetailList { get; set; }

        public PreContractLog()
		{
			preContractLogDetailList = new List<PreContractLogDetail>();
		}

		public void SetLogId() 
		{
            foreach (var item in preContractLogDetailList)
            {
				item.log_id = this.log_id;
            }
		}

		public PreContractLog(string file_name, int? number_record, int? contract_type, int? state, int? register_user_id, string register_user_fullname, DateTime? register_datetime)
		{
			this.file_name = file_name;
			this.number_record = number_record;
			this.contract_type = contract_type;
			this.state = state;
			this.register_user_id = register_user_id;
			this.register_user_fullname = register_user_fullname;
			this.register_datetime = register_datetime;
		}

		public PreContractLog(int log_id, string file_name, int? number_record, int? contract_type, int? state, int? register_user_id, string register_user_fullname, DateTime? register_datetime)
		{
			this.log_id = log_id;
			this.file_name = file_name;
			this.number_record = number_record;
			this.contract_type = contract_type;
			this.state = state;
			this.register_user_id = register_user_id;
			this.register_user_fullname = register_user_fullname;
			this.register_datetime = register_datetime;
		}

        public PreContractLog(int log_id, int? state, int? register_user_id, string register_user_fullname, DateTime? register_datetime)
        {
			this.log_id = log_id;
			this.state = state;
			this.register_user_id = register_user_id;
			this.register_user_fullname = register_user_fullname;
			this.register_datetime = register_datetime;
		}

	}
}
