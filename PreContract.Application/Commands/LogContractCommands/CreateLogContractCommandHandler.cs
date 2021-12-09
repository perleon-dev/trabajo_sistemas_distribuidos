using MediatR;

using System.Threading;
using System.Threading.Tasks;

using PreContracts.Api.Domain.Aggregates.LogContractAggregate;

namespace PreContracts.Api.Application.Commands.LogContractCommand
{
	public class CreateLogContractCommandHandler : IRequestHandler<CreateLogContractCommand, int>
	{
		readonly ILogContractRepository _iLogContractRepository;

		public CreateLogContractCommandHandler(ILogContractRepository iLogContractRepository)
		{
			_iLogContractRepository = iLogContractRepository;
		}

		public async Task<int> Handle(CreateLogContractCommand request, CancellationToken cancellationToken)
		{
			LogContract logContract = new LogContract( request.typeProcessId, request.fileStorageId, request.state, request.errorMessage, request.registerUserId, request.registerUserFullname, request.registerDatetime, request.updateUserId, request.updateUserFullname, request.updateDatetime);

			var result = await _iLogContractRepository.Register(logContract);

			return result;
		}
	}
}