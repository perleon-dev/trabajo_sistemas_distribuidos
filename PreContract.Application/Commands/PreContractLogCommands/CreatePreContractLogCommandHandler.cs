using Contracts.Api.Application.Queries.Generic;
using Contracts.Api.Domain.Aggregates.PreContractLogAggregate;
using Contracts.Api.Domain.Aggregates.PreContractLogDetailAggregate;
using Contracts.Api.Domain.Util;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Contracts.Api.Application.Commands.PreContractLogCommands
{
	public class CreatePreContractLogCommandHandler : IRequestHandler<CreatePreContractLogCommand, int>
	{
		readonly IPreContractLogRepository _iPreContractLogRepository;
		readonly IValuesSettingsApi _iValuesSettingsApi;
		public CreatePreContractLogCommandHandler(IPreContractLogRepository iPreContractLogRepository, IValuesSettingsApi iValuesSettingsApi)
		{
			_iPreContractLogRepository = iPreContractLogRepository;
			_iValuesSettingsApi = iValuesSettingsApi;
		}

		public async Task<int> Handle(CreatePreContractLogCommand request, CancellationToken cancellationToken)
		{
			PreContractLog preContractLog = new PreContractLog(request.file_name, request.number_record, request.contract_type, request.state, request.register_user_id, request.register_user_fullname, DateTime.Now.Peru(_iValuesSettingsApi.GetTimeZone()));
			List<PreContractLogDetail> preContractLogDetails = new List<PreContractLogDetail>();

			preContractLog.preContractLogDetailList = new List<PreContractLogDetail>();
            if (request.contract_type == ContractTypePreContract.VTex)
            {
                foreach (var item in request.createPreContractLogDetailCommands)
                {
					preContractLogDetails.Add(new PreContractLogDetail(item.document_id, item.id_summa, item.category_id, item.fixed_commisison_amount, item.start_date_contract, item.state, request.register_user_id, request.register_user_fullname, DateTime.Now.Peru(_iValuesSettingsApi.GetTimeZone()), item.observation));
				}
			}
            else if (request.contract_type == ContractTypePreContract.SellerCenter)
            {
                foreach (var item in request.createPreContractLogDetailCommands)
                {
					preContractLogDetails.Add(new PreContractLogDetail(item.document_id, null, item.id_summa, item.item, item.segment, item.commission_variable, null, item.category_name, item.validity, item.commisison_type, item.month_range_commission_variable, item.percentage_commission_variable, item.fixed_commission, item.month_range_fixed_commission, item.fixed_commisison_amount, item.start_date_contract, item.bank_name, item.bank_account, item.interbank_account, item.currency_name, item.bank_account_type_name, item.observation, item.state, item.log_id, item.register_user_id, item.register_user_fullname, DateTime.Now.Peru(_iValuesSettingsApi.GetTimeZone())));
				}
            }
			else if (request.contract_type == ContractTypePreContract.VTexToVTex)
			{
				foreach (var item in request.createPreContractLogDetailCommands)
				{
					preContractLogDetails.Add(new PreContractLogDetail(item.document_id, item.id_summa, item.item, item.segment, item.commission_variable ?? 0, item.category_id, item.category_name, item.month_range_commission_variable, item.percentage_commission_variable, item.start_date_contract, item.bank_name, item.bank_account, item.interbank_account, item.currency_name, item.bank_account_type_name, item.observation, item.state, item.register_user_id, item.register_user_fullname, DateTime.Now.Peru(_iValuesSettingsApi.GetTimeZone())));
				}
			}

			preContractLog.preContractLogDetailList = preContractLogDetails;

			var result = await _iPreContractLogRepository.Register(preContractLog);

			return result;
		}
	}
}
