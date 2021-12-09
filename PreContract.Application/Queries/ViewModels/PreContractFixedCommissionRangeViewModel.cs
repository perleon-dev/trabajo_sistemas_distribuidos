using System;
using System.Collections.Generic;
using System.Text;

namespace PreContracts.Api.Application.Queries.ViewModels
{
	public class PreContractFixedCommissionRangeViewModel
	{
		public int contract_fixed_com_range_id { get; set; }
		public int contract_id { get; set; }
		public int contract_version { get; set; }
		public int contract_modification { get; set; }
		public int validity_time { get; set; }
		public decimal amount { get; set; }
		public DateTime? validity_start_date { get; set; }
		public DateTime? validity_end_date { get; set; }
		public bool? validity_active { get; set; }
		public int state { get; set; }
		public int grade { get; set; }
		public int register_user_id { get; set; }
		public string register_user_fullname { get; set; }
		public DateTime register_datetime { get; set; }
		public int? update_user_id { get; set; }
		public string update_user_fullname { get; set; }
		public DateTime? update_datetime { get; set; }
	}

	public class PreContractFixedCommissionRangeRequest : Pagination
	{
		public int? contract_fixed_com_range_id { get; set; }
		public int? state { get; set; }
	}
}
