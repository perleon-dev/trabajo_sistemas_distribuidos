using Contracts.Api.Application.Queries.Generic;
using Contracts.Api.Domain.Aggregates.PreContractEconomicConditionAggregate;
using Contracts.Api.Domain.Util;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Contracts.Api.Application.Commands.PreContractEconomicConditionCommands
{
	public class CreatePreContractEconomicConditionCommandHandler : IRequestHandler<CreatePreContractEconomicConditionCommand, int>
	{
		readonly IPreContractEconomicConditionRepository _iPreContractEconomicConditionRepository;
		readonly IValuesSettingsApi _iValuesSettingsApi;
		public CreatePreContractEconomicConditionCommandHandler(IPreContractEconomicConditionRepository iPreContractEconomicConditionRepository, IValuesSettingsApi iValuesSettingsApi)
		{
			_iPreContractEconomicConditionRepository = iPreContractEconomicConditionRepository;
			_iValuesSettingsApi = iValuesSettingsApi;
		}

		public async Task<int> Handle(CreatePreContractEconomicConditionCommand request, CancellationToken cancellationToken)
		{
			PreContractEconomicCondition preContractEconomicCondition = new PreContractEconomicCondition(request.commission, request.category_id, request.state, request.contract_id, request.contract_version, request.contract_modification, request.register_user_id, request.register_user_fullname, DateTime.Now.Peru(_iValuesSettingsApi.GetTimeZone()), request.update_user_id, request.update_user_fullname, DateTime.Now.Peru(_iValuesSettingsApi.GetTimeZone()));

			var result = await _iPreContractEconomicConditionRepository.Register(preContractEconomicCondition);

			return result;
		}
	}
}
