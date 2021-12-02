using Contracts.Api.Domain.Util;
using MediatR;
using System.Collections.Generic;

namespace Contracts.Api.Application.Commands.PreContractCommands
{
	public class UpdateMassiveStatePreContractCommand : IRequest<MessageResponse>
	{
		public List<UpdateMassiveStatePreContract> preContractList { get; set; }
		public int? state { get; set; }
		public int updateUserId { get; set; }
		public string updateUserFullname { get; set; }
	}

	public class UpdateMassiveStatePreContract
	{
		public int contractId { get; set; }
		public int contractVersion { get; set; }
		public int contractModification { get; set; }
	}
}
