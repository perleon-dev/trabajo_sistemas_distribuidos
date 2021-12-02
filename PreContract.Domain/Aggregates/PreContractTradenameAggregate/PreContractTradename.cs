using Contracts.Api.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Api.Domain.Aggregates.PreContractTradenameAggregate
{
	public class PreContractTradename : Entity
	{
		public int contract_tradename_id { get; set; }
		public int? contract_id { get; set; }
		public int? contract_version { get; set; }
		public int? contract_modification { get; set; }
		public int? tradename_id { get; }
		public string id_summa { get; set; }
		public int? rubric_id { get; }
		public int? register_user_id { get; }
		public string register_user_fullname { get; }
		public DateTime? register_datetime { get; }
		public int? update_user_id { get; }
		public string update_user_fullname { get; }
		public DateTime? update_datetime { get; }
		public int? state { get; }

		public PreContractTradename()
		{
		}

		public PreContractTradename(int? contract_id, int? contract_version, int? contract_modification, int? tradename_id, int? rubric_id, int? register_user_id, string register_user_fullname, DateTime? register_datetime, int? update_user_id, string update_user_fullname, DateTime? update_datetime, int? state, string id_summa)
		{
			this.contract_id = contract_id;
			this.contract_version = contract_version;
			this.contract_modification = contract_modification;
			this.tradename_id = tradename_id;
			this.rubric_id = rubric_id;
			this.register_user_id = register_user_id;
			this.register_user_fullname = register_user_fullname;
			this.register_datetime = register_datetime;
			this.update_user_id = update_user_id;
			this.update_user_fullname = update_user_fullname;
			this.update_datetime = update_datetime;
			this.state = state;
			this.id_summa = id_summa;
		}

		public PreContractTradename(int contract_tradename_id, int? contract_id, int? contract_version, int? contract_modification, int? tradename_id, int? rubric_id, int? register_user_id, string register_user_fullname, DateTime? register_datetime, int? update_user_id, string update_user_fullname, DateTime? update_datetime, int? state, string id_summa)
		{
			this.contract_tradename_id = contract_tradename_id;
			this.contract_id = contract_id;
			this.contract_version = contract_version;
			this.contract_modification = contract_modification;
			this.tradename_id = tradename_id;
			this.rubric_id = rubric_id;
			this.register_user_id = register_user_id;
			this.register_user_fullname = register_user_fullname;
			this.register_datetime = register_datetime;
			this.update_user_id = update_user_id;
			this.update_user_fullname = update_user_fullname;
			this.update_datetime = update_datetime;
			this.state = state;
			this.id_summa = id_summa;
		}

		public PreContractTradename(int? tradename_id, int? update_user_id, string update_user_fullname, int? state)
		{
			this.tradename_id = tradename_id;
			this.update_user_id = update_user_id;
			this.update_user_fullname = update_user_fullname;
			this.state = state;
		}
	}
}
