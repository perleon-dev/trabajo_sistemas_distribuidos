using System;

namespace PreContracts.Api.Application.Queries.ViewModels
{
	public class PreContractLogViewModel
	{
		public int log_id { get; set; }
		public string file_name { get; set; }
		public int? number_record { get; set; }
		public int? contract_type { get; set; }
		public int? state { get; set; }
		public int? register_user_id { get; set; }
		public string register_user_fullname { get; set; }
		public DateTime? register_datetime { get; set; }
	}

	public class PreContractLogRequest : Pagination
	{
		public int? log_id { get; set; }
        public string file_name { get; set; }
        public int? state { get; set; }
    }
}
