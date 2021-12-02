using Contracts.Api.Application.Queries.Generic;
using Contracts.Api.Domain.Aggregates.PreContractFixedCommissionRangeAggregate;
using Contracts.Api.Domain.Util;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Contracts.Api.Application.Commands.PreContractFixedCommissionRangeCommands
{
	public class CreatePreContractFixedCommissionRangeCommandHandler : IRequestHandler<CreatePreContractFixedCommissionRangeCommand, int>
	{
		readonly IPreContractFixedCommissionRangeRepository _iPreContractFixedCommissionRangeRepository;
		readonly IValuesSettings _iValuesSettings;
		public CreatePreContractFixedCommissionRangeCommandHandler(IPreContractFixedCommissionRangeRepository iPreContractFixedCommissionRangeRepository, IValuesSettings iValuesSettings)
		{
			_iPreContractFixedCommissionRangeRepository = iPreContractFixedCommissionRangeRepository;
			_iValuesSettings = iValuesSettings;
		}

		public async Task<int> Handle(CreatePreContractFixedCommissionRangeCommand request, CancellationToken cancellationToken)
		{
			PreContractFixedCommissionRange preContractFixedCommissionRange = new PreContractFixedCommissionRange(request.contract_id, request.contract_version, request.contract_modification, request.validity_time, request.amount, request.validity_start_date, request.validity_end_date, request.validity_active, request.state, request.grade, request.register_user_id, request.register_user_fullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()), request.update_user_id, request.update_user_fullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()));

			var result = await _iPreContractFixedCommissionRangeRepository.Register(preContractFixedCommissionRange);

			return result;
		}
	}
}
