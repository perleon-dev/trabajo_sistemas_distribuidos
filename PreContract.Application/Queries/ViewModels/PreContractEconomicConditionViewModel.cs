using System;
using System.Collections.Generic;
using System.Text;

namespace PreContracts.Api.Application.Queries.ViewModels
{
	public class PreContractEconomicConditionViewModel
	{
		public int economic_condition_id { get; set; }
		public decimal? commission { get; set; }
		public int? category_id { get; set; }
		public int? state { get; set; }
		public int? contract_id { get; set; }
		public int? contract_version { get; set; }
		public int? contract_modification { get; set; }
		public int? register_user_id { get; set; }
		public string register_user_fullname { get; set; }
		public DateTime? register_datetime { get; set; }
		public int? update_user_id { get; set; }
		public string update_user_fullname { get; set; }
		public DateTime? update_datetime { get; set; }
	}

	public class PreContractEconomicConditionRequest : Pagination
	{
		public int? economic_condition_id { get; set; }
		public int? state { get; set; }
	}
}
