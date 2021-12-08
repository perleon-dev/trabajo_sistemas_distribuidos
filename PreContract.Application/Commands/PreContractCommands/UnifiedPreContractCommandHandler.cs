using Contracts.Api.Application.Queries.Generic;
using Contracts.Api.Domain.Aggregates.PreContractAggregate;
using Contracts.Api.Domain.Aggregates.PreContractBankAccountAggregate;
using Contracts.Api.Domain.Aggregates.PreContractEconomicConditionAggregate;
using Contracts.Api.Domain.Aggregates.PreContractFixedCommissionRangeAggregate;
using Contracts.Api.Domain.Aggregates.PreContractTradenameAggregate;
using Contracts.Api.Domain.Aggregates.PreContractVariableCommissionRangeAggregate;
using Contracts.Api.Domain.Util;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Contracts.Api.Application.Commands.PreContractCommands
{
	public class UnifiedPreContractCommandHandler : IRequestHandler<UnifiedPreContractCommand, int>
	{
		readonly IPreContractRepository _iPreContractRepository;
		readonly IValuesSettingsApi _iValuesSettingsApi;

		public UnifiedPreContractCommandHandler(IPreContractRepository iPreContractRepository, IValuesSettingsApi iValuesSettingsApi)
		{
			_iPreContractRepository = iPreContractRepository;
			_iValuesSettingsApi = iValuesSettingsApi;
		}

		public async Task<int> Handle(UnifiedPreContractCommand request, CancellationToken cancellationToken)
		{
			var preContractCommand = request.preContract;
			Contracts.Api.Domain.Aggregates.PreContractAggregate.PreContract preContract = new Contracts.Api.Domain.Aggregates.PreContractAggregate.PreContract(preContractCommand.contract_version, preContractCommand.contract_modification, preContractCommand.contract_start_date, preContractCommand.contract_end_date, preContractCommand.ruc, preContractCommand.bank_account, preContractCommand.cci, preContractCommand.code_contract, preContractCommand.type_seller, preContractCommand.distribution_type, preContractCommand.product_commission, preContractCommand.state, preContractCommand.active, preContractCommand.tradename_id, preContractCommand.mall_id, preContractCommand.bank_id, preContractCommand.type_currency, preContractCommand.bank_account_type, preContractCommand.segment_id, preContractCommand.origin, preContractCommand.ubigeo, preContractCommand.commercial_template_id, preContractCommand.register_user_id, preContractCommand.register_user_fullname, DateTime.Now.Peru(_iValuesSettingsApi.GetTimeZone()), preContractCommand.update_user_id, preContractCommand.update_user_fullname, null);
			
			var bankAccount = GetBankAccount(request);
			var tradenames = GetTradenames(request);
			var fixedCommissions = GetFixedCommissions(request);
			var variableCommissions = GetVariableCommissions(request);
			var economicConditions = GetEconomicConditions(request);

			var result = await _iPreContractRepository.RegisterUnified(preContract, bankAccount, tradenames,fixedCommissions,variableCommissions, economicConditions);

			return result;
		}

		private PreContractBankAccount GetBankAccount(UnifiedPreContractCommand request)
		{
			if (request.bankAccount != null)
			{
				return new PreContractBankAccount(
					   request.bankAccount.contract_id,
					   request.bankAccount.contract_version,
					   request.bankAccount.contract_modification,
					   request.bankAccount.bank_id,
					   request.bankAccount.currency_id,
					   request.bankAccount.account_number,
					   request.bankAccount.cci_account_number,
					   request.bankAccount.type_account,
					   request.bankAccount.register_user_id,
					   request.bankAccount.register_user_fullname,
					   DateTime.Now.Peru(_iValuesSettingsApi.GetTimeZone()),
					   request.bankAccount.update_user_id,
					   request.bankAccount.update_user_fullname,
					   null,
					   request.bankAccount.state
					   );
			}

			return null;
		}

		private List<PreContractTradename> GetTradenames(UnifiedPreContractCommand request)
		{
			var tradenames = new List<PreContractTradename>();

			if (request.tradenames != null)
			{
				foreach (var tradenameCommand in request.tradenames)
				{
					tradenames.Add(new PreContractTradename(
						tradenameCommand.contract_id,
						tradenameCommand.contract_version,
						tradenameCommand.contract_modification,
						tradenameCommand.tradename_id,
						tradenameCommand.rubric_id,
						tradenameCommand.register_user_id,
						tradenameCommand.register_user_fullname,
						DateTime.Now.Peru(_iValuesSettingsApi.GetTimeZone()),
						tradenameCommand.update_user_id,
						tradenameCommand.update_user_fullname,
						null,
						tradenameCommand.state,
						tradenameCommand.id_summa
						));
				}
			}

			return tradenames;
		}

		private List<PreContractFixedCommissionRange>  GetFixedCommissions(UnifiedPreContractCommand request)
		{
			var fixedCommissions = new List<PreContractFixedCommissionRange>();

			if (request.fixedCommissions != null)
			{
				foreach (var fixedCommission in request.fixedCommissions)
				{
					fixedCommissions.Add(new PreContractFixedCommissionRange(
						fixedCommission.contract_id,
						fixedCommission.contract_version,
						fixedCommission.contract_modification,
						fixedCommission.validity_time,
						fixedCommission.amount,
						fixedCommission.validity_start_date,
						fixedCommission.validity_end_date,
						fixedCommission.validity_active,
						fixedCommission.state,
						fixedCommission.grade,
						fixedCommission.register_user_id,
						fixedCommission.register_user_fullname,
						DateTime.Now.Peru(_iValuesSettingsApi.GetTimeZone()),
						fixedCommission.update_user_id,
						fixedCommission.update_user_fullname,
						null
						));
				}
			}

			return fixedCommissions;
		}

		private List<PreContractVariableCommissionRange> GetVariableCommissions(UnifiedPreContractCommand request)
		{
			var variableCommissions = new List<PreContractVariableCommissionRange>();

			if (request.variableCommissions != null)
			{
				foreach (var variableCommission in request.variableCommissions)
				{
					variableCommissions.Add(new PreContractVariableCommissionRange(
						variableCommission.contract_id,
						variableCommission.contract_version,
						variableCommission.contract_modification,
						variableCommission.contract_tradename_id,
						variableCommission.state,
						variableCommission.validity_time,
						variableCommission.validity_active,
						variableCommission.percentage,
						variableCommission.validity_start_date,
						variableCommission.validity_end_date,
						variableCommission.grade,
						variableCommission.register_user_id,
						variableCommission.register_user_fullname,
						DateTime.Now.Peru(_iValuesSettingsApi.GetTimeZone()),
						variableCommission.update_user_id,
						variableCommission.update_user_fullname,
						null,
						variableCommission.category_id
						));
				}
			}

			return variableCommissions;
		}

		private List<PreContractEconomicCondition> GetEconomicConditions(UnifiedPreContractCommand request)
		{
			var economicConditions = new List<PreContractEconomicCondition>();

			if (request.economicConditions != null)
			{
				foreach (var economicCondition in request.economicConditions)
				{
					economicConditions.Add(new PreContractEconomicCondition(
						economicCondition.commission,
						economicCondition.category_id,
						economicCondition.state,
						economicCondition.contract_id,
						economicCondition.contract_version,
						economicCondition.contract_modification,
						economicCondition.register_user_id,
						economicCondition.register_user_fullname,
						DateTime.Now.Peru(_iValuesSettingsApi.GetTimeZone()),
						economicCondition.update_user_id,
						economicCondition.update_user_fullname,
						null
						));
				}
			}

			return economicConditions;
		}
	}
}
