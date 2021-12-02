using MediatR;

namespace Contracts.Api.Application.Commands.PreContractLogCommands
{
	public class UpdatePreContractLogCommand : IRequest<int>
	{
		public int log_id { get; set; }
		public string file_name { get; set; }
		public int? number_record { get; set; }
		public int? contract_type { get; set; }
		public int? state { get; set; }
		public int? register_user_id { get; set; }
		public string register_user_fullname { get; set; }
	}
}
