using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Api.Application.Commands.PreContractVariableCommissionRangeCommand
{
	public class UpdatePreContractVariableCommissionRangeCommand : IRequest<int>
	{
		public int contract_variable_com_range_id { get; set; }
		public int contract_id { get; set; }
		public int contract_version { get; set; }
		public int contract_modification { get; set; }
		public int contract_tradename_id { get; set; }
		public int state { get; set; }
		public int validity_time { get; set; }
		public bool? validity_active { get; set; }
		public decimal percentage { get; set; }
		public DateTime? validity_start_date { get; set; }
		public DateTime? validity_end_date { get; set; }
		public int grade { get; set; }
		public int? register_user_id { get; set; }
		public string register_user_fullname { get; set; }
		public int? update_user_id { get; set; }
		public string update_user_fullname { get; set; }
		public int? category_id { get; set; }
	}
}
