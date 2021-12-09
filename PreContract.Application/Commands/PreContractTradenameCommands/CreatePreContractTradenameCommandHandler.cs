using PreContracts.Api.Application.Queries.Generic;
using PreContracts.Api.Domain.Aggregates.PreContractTradenameAggregate;
using PreContracts.Api.Domain.Util;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PreContracts.Api.Application.Commands.PreContractTradenameCommands
{
	public class CreatePreContractTradenameCommandHandler : IRequestHandler<CreatePreContractTradenameCommand, int>
	{
		readonly IPreContractTradenameRepository _iPreContractTradenameRepository;
		readonly IValuesSettings _iValuesSettings;
		public CreatePreContractTradenameCommandHandler(IPreContractTradenameRepository iPreContractTradenameRepository, IValuesSettings iValuesSettings)
		{
			_iPreContractTradenameRepository = iPreContractTradenameRepository;
			_iValuesSettings = iValuesSettings;
		}

		public async Task<int> Handle(CreatePreContractTradenameCommand request, CancellationToken cancellationToken)
		{
			PreContractTradename preContractTradename = new PreContractTradename(request.contract_id, request.contract_version, request.contract_modification, request.tradename_id, request.rubric_id, request.register_user_id, request.register_user_fullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()), request.update_user_id, request.update_user_fullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()), request.state, request.id_summa);

			var result = await _iPreContractTradenameRepository.Register(preContractTradename);

			return result;
		}
	}
}
