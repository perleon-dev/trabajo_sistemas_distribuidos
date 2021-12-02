using System;

namespace Contracts.Api.Application.Queries.ViewModels
{
	public class PreContractLogDetailViewModel
	{
		public int log_detail_id { get; set; }
		public string document_id { get; set; }
		public string business_name { get; set; }
		public string id_summa { get; set; }
		public string item { get; set; }
		public string segment { get; set; }
		public int? commission_variable { get; set; }
		public int? category_id { get; set; }
		public string category_name { get; set; }
		public string validity { get; set; }
		public string commisison_type { get; set; }
		public int? month_range_commission_variable { get; set; }
		public decimal? percentage_commission_variable { get; set; }
		public int? fixed_commission { get; set; }
		public int? month_range_fixed_commission { get; set; }
		public decimal? fixed_commisison_amount { get; set; }
		public string start_date_contract { get; set; }
		public string bank_name { get; set; }
		public string bank_account { get; set; }
		public string interbank_account { get; set; }
		public string currency_name { get; set; }
		public string bank_account_type_name { get; set; }
		public string observation { get; set; }
		public int? state { get; set; }
		public int? log_id { get; set; }
		public int? register_user_id { get; set; }
		public string register_user_fullname { get; set; }
		public DateTime? register_datetime { get; set; }
	}

	public class PreContractLogDetailRequest : Pagination
	{
		public int? log_detail_id { get; set; }
        public int? log_id { get; set; }
        public int? state { get; set; }
    }
}
