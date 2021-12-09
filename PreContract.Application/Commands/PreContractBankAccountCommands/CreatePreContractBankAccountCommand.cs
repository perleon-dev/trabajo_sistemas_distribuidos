using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreContracts.Api.Application.Commands.PreContractBankAccountCommands
{
	public class CreatePreContractBankAccountCommand : IRequest<int>
	{
		public int? contract_id { get; set; }
		public int? contract_version { get; set; }
		public int? contract_modification { get; set; }
		public int? bank_id { get; set; }
		public int? currency_id { get; set; }
		public string account_number { get; set; }
		public string cci_account_number { get; set; }
		public int? type_account { get; set; }
        public int? state { get; set; }
        public int? register_user_id { get; set; }
		public string register_user_fullname { get; set; }
		public int? update_user_id { get; set; }
		public string update_user_fullname { get; set; }
	}
}
