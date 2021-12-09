using PreContracts.Api.Application.Queries.Generic;
using PreContracts.Api.Domain.Aggregates.PreContractLogDetailAggregate;
using PreContracts.Api.Domain.Util;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PreContracts.Api.Application.Commands.PreContractLogDetailCommand
{
	public class UpdateMassivePreContractLogDetailCommandHandler : IRequestHandler<UpdateMassivePreContractLogDetailCommand, int>
	{
		readonly IPreContractLogDetailRepository _iPreContractLogDetailRepository;
		readonly IValuesSettingsApi _iValuesSettingsApi;

		public UpdateMassivePreContractLogDetailCommandHandler(IPreContractLogDetailRepository iPreContractLogDetailRepository, IValuesSettingsApi iValuesSettingsApi)
		{
			_iPreContractLogDetailRepository = iPreContractLogDetailRepository;
			_iValuesSettingsApi = iValuesSettingsApi;
		}

		public async Task<int> Handle(UpdateMassivePreContractLogDetailCommand request, CancellationToken cancellationToken)
		{
			var details = new List<PreContractLogDetail>();

			if (request.logDetails != null)
			{
				foreach (var logDetail in request.logDetails)
					details.Add(new PreContractLogDetail(logDetail.log_detail_id,
						logDetail.document_id, 
						logDetail.business_name, 
						logDetail.id_summa, 
						logDetail.item, 
						logDetail.segment, 
						logDetail.commission_variable, 
						logDetail.category_id, 
						logDetail.category_name, 
						logDetail.validity, 
						logDetail.commisison_type, 
						logDetail.month_range_commission_variable, 
						logDetail.percentage_commission_variable, 
						logDetail.fixed_commission, 
						logDetail.month_range_fixed_commission, 
						logDetail.fixed_commisison_amount, 
						logDetail.start_date_contract, 
						logDetail.bank_name, 
						logDetail.bank_account, 
						logDetail.interbank_account, 
						logDetail.currency_name, 
						logDetail.bank_account_type_name, 
						logDetail.observation, 
						logDetail.state, 
						logDetail.log_id, 
						logDetail.register_user_id, 
						logDetail.register_user_fullname, 
						DateTime.Now.Peru(_iValuesSettingsApi.GetTimeZone())));

				var result = await _iPreContractLogDetailRepository.RegisterAsyncJson(details);
				return result;
			}

			return 0;
		}
	}
}
