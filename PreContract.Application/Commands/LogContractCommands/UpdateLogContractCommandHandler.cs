using MediatR;

using System.Threading;
using System.Threading.Tasks;

using Contracts.Api.Domain.Aggregates.LogContractAggregate;

namespace Contracts.Api.Application.Commands.LogContractCommand
{
	public class UpdateLogContractCommandHandler : IRequestHandler<UpdateLogContractCommand, int>
	{
		readonly ILogContractRepository _iLogContractRepository;

		public UpdateLogContractCommandHandler(ILogContractRepository iLogContractRepository)
		{
			_iLogContractRepository = iLogContractRepository;
		}

		public async Task<int> Handle(UpdateLogContractCommand request, CancellationToken cancellationToken)
		{
			LogContract logContract = new LogContract( request.logContractId, request.typeProcessId, request.fileStorageId, request.state, request.errorMessage, request.registerUserId, request.registerUserFullname, request.registerDatetime, request.updateUserId, request.updateUserFullname, request.updateDatetime);

			var result = await _iLogContractRepository.Register(logContract);

			return result;
		}
	}
}