using System;

namespace Contracts.Api.Application.Queries.ViewModels
{
	public class PreContractTradenameViewModel
	{
		public int contract_tradename_id { get; set; }
		public int? contract_id { get; set; }
		public int? contract_version { get; set; }
		public int? contract_modification { get; set; }
		public int? tradename_id { get; set; }
		public string id_summa { get; set; }
		public int? rubric_id { get; set; }
		public int? register_user_id { get; set; }
		public string register_user_fullname { get; set; }
		public DateTime? register_datetime { get; set; }
		public int? update_user_id { get; set; }
		public string update_user_fullname { get; set; }
		public DateTime? update_datetime { get; set; }
		public int? state { get; set; }
        public string tradename { get; set; }
    }

	public class PreContractTradenameRequest : Pagination
	{
		public int? contract_tradename_id { get; set; }
		public int? state { get; set; }
		public string ruc { get; set; }
	}
}
