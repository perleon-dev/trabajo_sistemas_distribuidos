using PreContracts.Api.Application.Queries.Generic;
using PreContracts.Api.Domain.Aggregates.PreContractLogDetailAggregate;
using PreContracts.Api.Domain.Util;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PreContracts.Api.Application.Commands.PreContractLogDetailCommand
{
	public class CreatePreContractLogDetailCommandHandler : IRequestHandler<CreatePreContractLogDetailCommand, int>
	{
		readonly IPreContractLogDetailRepository _iPreContractLogDetailRepository;
		readonly IValuesSettingsApi _iValuesSettingsApi;
		public CreatePreContractLogDetailCommandHandler(IPreContractLogDetailRepository iPreContractLogDetailRepository, IValuesSettingsApi iValuesSettingsApi)
		{
			_iPreContractLogDetailRepository = iPreContractLogDetailRepository;
			_iValuesSettingsApi = iValuesSettingsApi;
		}

		public async Task<int> Handle(CreatePreContractLogDetailCommand request, CancellationToken cancellationToken)
		{
			PreContractLogDetail preContractLogDetail = new PreContractLogDetail(request.document_id, request.business_name, request.id_summa, request.item, request.segment, request.commission_variable, request.category_id, request.category_name, request.validity, request.commisison_type, request.month_range_commission_variable, request.percentage_commission_variable, request.fixed_commission, request.month_range_fixed_commission, request.fixed_commisison_amount, request.start_date_contract, request.bank_name, request.bank_account, request.interbank_account, request.currency_name, request.bank_account_type_name, request.observation, request.state, request.log_id, request.register_user_id, request.register_user_fullname, DateTime.Now.Peru(_iValuesSettingsApi.GetTimeZone()));

			var result = await _iPreContractLogDetailRepository.Register(preContractLogDetail);

			return result;
		}
	}
}
