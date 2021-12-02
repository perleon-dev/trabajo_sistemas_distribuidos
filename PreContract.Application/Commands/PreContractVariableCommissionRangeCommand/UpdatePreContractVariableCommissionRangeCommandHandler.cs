using Contracts.Api.Application.Queries.Generic;
using Contracts.Api.Domain.Aggregates.PreContractVariableCommissionRangeAggregate;
using Contracts.Api.Domain.Util;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Contracts.Api.Application.Commands.PreContractVariableCommissionRangeCommand
{
	public class UpdatePreContractVariableCommissionRangeCommandHandler : IRequestHandler<UpdatePreContractVariableCommissionRangeCommand, int>
	{
		readonly IPreContractVariableCommissionRangeRepository _iPreContractVariableCommissionRangeRepository;
		readonly IValuesSettings _iValuesSettings;
		public UpdatePreContractVariableCommissionRangeCommandHandler(IPreContractVariableCommissionRangeRepository iPreContractVariableCommissionRangeRepository, IValuesSettings iValuesSettings)
		{
			_iPreContractVariableCommissionRangeRepository = iPreContractVariableCommissionRangeRepository;
			_iValuesSettings = iValuesSettings;
		}

		public async Task<int> Handle(UpdatePreContractVariableCommissionRangeCommand request, CancellationToken cancellationToken)
		{
			PreContractVariableCommissionRange preContractVariableCommissionRange = new PreContractVariableCommissionRange(request.contract_variable_com_range_id, request.contract_id, request.contract_version, request.contract_modification, request.contract_tradename_id, request.state, request.validity_time, request.validity_active, request.percentage, request.validity_start_date, request.validity_end_date, request.grade, request.register_user_id, request.register_user_fullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()), request.update_user_id, request.update_user_fullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()), request.category_id);

			var result = await _iPreContractVariableCommissionRangeRepository.Register(preContractVariableCommissionRange);

			return result;
		}
	}
}
