using Contracts.Api.Application.Queries.Generic;
using Contracts.Api.Domain.Aggregates.PreContractBankAccountAggregate;
using Contracts.Api.Domain.Util;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Contracts.Api.Application.Commands.PreContractBankAccountCommands
{
	public class UpdatePreContractBankAccountCommandHandler : IRequestHandler<UpdatePreContractBankAccountCommand, int>
	{
		readonly IPreContractBankAccountRepository _iPreContractBankAccountRepository;
		readonly IValuesSettings _iValuesSettings;
		public UpdatePreContractBankAccountCommandHandler(IPreContractBankAccountRepository iPreContractBankAccountRepository, IValuesSettings ivaluesSettings)
		{
			_iPreContractBankAccountRepository = iPreContractBankAccountRepository;
			_iValuesSettings = ivaluesSettings;
		}

		public async Task<int> Handle(UpdatePreContractBankAccountCommand request, CancellationToken cancellationToken)
		{
			PreContractBankAccount preContractBankAccount = new PreContractBankAccount(request.contract_bank_account_id, request.contract_id, request.contract_version, request.contract_modification, request.bank_id, request.currency_id, request.account_number, request.cci_account_number, request.type_account, request.register_user_id, request.register_user_fullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()), request.update_user_id, request.update_user_fullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()), request.state);

			var result = await _iPreContractBankAccountRepository.Register(preContractBankAccount);

			return result;
		}
	}
}
