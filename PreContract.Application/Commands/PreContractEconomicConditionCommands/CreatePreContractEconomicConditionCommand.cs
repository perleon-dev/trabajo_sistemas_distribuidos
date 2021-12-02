using MediatR;

namespace Contracts.Api.Application.Commands.PreContractEconomicConditionCommands
{
	public class CreatePreContractEconomicConditionCommand : IRequest<int>
	{
		public decimal? commission { get; set; }
		public int? category_id { get; set; }
		public int? state { get; set; }
		public int contract_id { get; set; }
		public int contract_version { get; set; }
		public int contract_modification { get; set; }
		public int? register_user_id { get; set; }
		public string register_user_fullname { get; set; }
		public int? update_user_id { get; set; }
		public string update_user_fullname { get; set; }
	}
}
