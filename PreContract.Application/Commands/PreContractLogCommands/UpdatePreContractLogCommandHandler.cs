using Contracts.Api.Application.Queries.Generic;
using Contracts.Api.Domain.Aggregates.PreContractLogAggregate;
using Contracts.Api.Domain.Util;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Contracts.Api.Application.Commands.PreContractLogCommands
{
	public class UpdatePreContractLogCommandHandler : IRequestHandler<UpdatePreContractLogCommand, int>
	{
		readonly IPreContractLogRepository _iPreContractLogRepository;
		readonly IValuesSettingsApi _iValuesSettingsApi;

		public UpdatePreContractLogCommandHandler(IPreContractLogRepository iPreContractLogRepository, IValuesSettingsApi iValuesSettingsApi)
		{
			_iPreContractLogRepository = iPreContractLogRepository;
			_iValuesSettingsApi = iValuesSettingsApi;
		}

		public async Task<int> Handle(UpdatePreContractLogCommand request, CancellationToken cancellationToken)
		{
			PreContractLog preContractLog = new PreContractLog(request.log_id, request.file_name, request.number_record, request.contract_type, request.state, request.register_user_id, request.register_user_fullname, DateTime.Now.Peru(_iValuesSettingsApi.GetTimeZone()));

			var result = await _iPreContractLogRepository.Register(preContractLog);

			return result;
		}
	}
}
