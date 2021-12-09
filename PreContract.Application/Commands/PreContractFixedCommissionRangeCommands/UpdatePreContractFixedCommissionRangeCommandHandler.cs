using PreContracts.Api.Application.Queries.Generic;
using PreContracts.Api.Domain.Aggregates.PreContractFixedCommissionRangeAggregate;
using PreContracts.Api.Domain.Util;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PreContracts.Api.Application.Commands.PreContractFixedCommissionRangeCommands
{
	public class UpdatePreContractFixedCommissionRangeCommandHandler : IRequestHandler<UpdatePreContractFixedCommissionRangeCommand, int>
	{
		readonly IPreContractFixedCommissionRangeRepository _iPreContractFixedCommissionRangeRepository;
		readonly IValuesSettings _iValuesSettings;
		public UpdatePreContractFixedCommissionRangeCommandHandler(IPreContractFixedCommissionRangeRepository iPreContractFixedCommissionRangeRepository, IValuesSettings iValuesSettings)
		{
			_iPreContractFixedCommissionRangeRepository = iPreContractFixedCommissionRangeRepository;
			_iValuesSettings = iValuesSettings;
		}

		public async Task<int> Handle(UpdatePreContractFixedCommissionRangeCommand request, CancellationToken cancellationToken)
		{
			PreContractFixedCommissionRange preContractFixedCommissionRange = new PreContractFixedCommissionRange(request.contract_fixed_com_range_id, request.contract_id, request.contract_version, request.contract_modification, request.validity_time, request.amount, request.validity_start_date, request.validity_end_date, request.validity_active, request.state, request.grade, request.register_user_id, request.register_user_fullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()), request.update_user_id, request.update_user_fullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()));

			var result = await _iPreContractFixedCommissionRangeRepository.Register(preContractFixedCommissionRange);

			return result;
		}
	}
}
