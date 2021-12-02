using MediatR;
using System;

namespace Contracts.Api.Application.Commands.PreContractCommands
{
	public class UpdatePreContractCommand : IRequest<int>
	{
		public int contract_id { get; set; }
		public int contract_version { get; set; }
		public int contract_modification { get; set; }
		public DateTime? contract_start_date { get; set; }
		public DateTime? contract_end_date { get; set; }
		public string ruc { get; set; }
		public string bank_account { get; set; }
		public string cci { get; set; }
		public string code_contract { get; set; }
		public int? type_seller { get; set; }
		public string distribution_type { get; set; }
		public decimal? product_commission { get; set; }
		public int? state { get; set; }
		public bool? active { get; set; }
		public int? tradename_id { get; set; }
		public int? mall_id { get; set; }
		public string bank_id { get; set; }
		public int? type_currency { get; set; }
		public string bank_account_type { get; set; }
		public int? segment_id { get; set; }
		public int? origin { get; set; }
		public string ubigeo { get; set; }
		public int? commercial_template_id { get; set; }
		public int? register_user_id { get; set; }
		public string register_user_fullname { get; set; }
		public int? update_user_id { get; set; }
		public string update_user_fullname { get; set; }
	}
}
