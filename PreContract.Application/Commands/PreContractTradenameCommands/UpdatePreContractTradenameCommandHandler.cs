using PreContracts.Api.Application.Queries.Generic;
using PreContracts.Api.Domain.Aggregates.PreContractTradenameAggregate;
using PreContracts.Api.Domain.Util;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PreContracts.Api.Application.Commands.PreContractTradenameCommands
{
	public class UpdatePreContractTradenameCommandHandler : IRequestHandler<UpdatePreContractTradenameCommand, int>
	{
		readonly IPreContractTradenameRepository _iPreContractTradenameRepository;
		readonly IValuesSettings _iValuesSettings;
		public UpdatePreContractTradenameCommandHandler(IPreContractTradenameRepository iPreContractTradenameRepository, IValuesSettings iValuesSettings)
		{
			_iPreContractTradenameRepository = iPreContractTradenameRepository;
			_iValuesSettings = iValuesSettings;
		}

		public async Task<int> Handle(UpdatePreContractTradenameCommand request, CancellationToken cancellationToken)
		{
			PreContractTradename preContractTradename = new PreContractTradename(request.contract_tradename_id, request.contract_id, request.contract_version, request.contract_modification, request.tradename_id, request.rubric_id, request.register_user_id, request.register_user_fullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()), request.update_user_id, request.update_user_fullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()), request.state, request.id_summa);

			var result = await _iPreContractTradenameRepository.Register(preContractTradename);

			return result;
		}
	}
}
