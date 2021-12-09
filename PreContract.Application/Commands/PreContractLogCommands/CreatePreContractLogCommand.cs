using PreContracts.Api.Application.Commands.PreContractLogDetailCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreContracts.Api.Application.Commands.PreContractLogCommands
{
	public class CreatePreContractLogCommand : IRequest<int>
	{
		public string file_name { get; set; }
		public int? number_record { get; set; }
		public int? contract_type { get; set; }
		public int? state { get; set; }
		public int? register_user_id { get; set; }
		public string register_user_fullname { get; set; }
        public List<CreatePreContractLogDetailCommand> createPreContractLogDetailCommands { get; set; }
    }
}
