using Contracts.Api.Application.Queries.Generic;
using Contracts.Api.Domain.Aggregates.PreContractAggregate;
using Contracts.Api.Domain.Util;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Contracts.Api.Application.Commands.PreContractCommands
{
	public class CreatePreContractCommandHandler : IRequestHandler<CreatePreContractCommand, int>
	{
		readonly IPreContractRepository _iPreContractRepository;
		readonly IValuesSettings _iValuesSettings;

		public CreatePreContractCommandHandler(IPreContractRepository iPreContractRepository, IValuesSettings iValuesSettings)
		{
			_iPreContractRepository = iPreContractRepository;
			_iValuesSettings = iValuesSettings;
		}

		public async Task<int> Handle(CreatePreContractCommand request, CancellationToken cancellationToken)
		{
			Contracts.Api.Domain.Aggregates.PreContractAggregate.PreContract preContract = new Contracts.Api.Domain.Aggregates.PreContractAggregate.PreContract(request.contract_version, request.contract_modification, request.contract_start_date, request.contract_end_date, request.ruc, request.bank_account, request.cci, request.code_contract, request.type_seller, request.distribution_type, request.product_commission, request.state, request.active, request.tradename_id, request.mall_id, request.bank_id, request.type_currency, request.bank_account_type, request.segment_id, request.origin, request.ubigeo, request.commercial_template_id, request.register_user_id, request.register_user_fullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()), request.update_user_id, request.update_user_fullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()));

			var result = await _iPreContractRepository.Register(preContract);

			return result;
		}
	}
}
