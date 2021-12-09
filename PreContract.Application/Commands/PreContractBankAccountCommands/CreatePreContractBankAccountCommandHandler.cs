using PreContracts.Api.Application.Queries.Generic;
using PreContracts.Api.Domain.Aggregates.PreContractBankAccountAggregate;
using PreContracts.Api.Domain.Util;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PreContracts.Api.Application.Commands.PreContractBankAccountCommands
{
	public class CreatePreContractBankAccountCommandHandler : IRequestHandler<CreatePreContractBankAccountCommand, int>
	{
		readonly IPreContractBankAccountRepository _iPreContractBankAccountRepository;
		readonly IValuesSettings _iValuesSettings;

		public CreatePreContractBankAccountCommandHandler(IPreContractBankAccountRepository iPreContractBankAccountRepository, IValuesSettings ivaluesSettings)
		{
			_iPreContractBankAccountRepository = iPreContractBankAccountRepository;
			_iValuesSettings = ivaluesSettings;
		}

		public async Task<int> Handle(CreatePreContractBankAccountCommand request, CancellationToken cancellationToken)
		{
			PreContractBankAccount preContractBankAccount = new PreContractBankAccount(request.contract_id, request.contract_version, request.contract_modification, request.bank_id, request.currency_id, request.account_number, request.cci_account_number, request.type_account, request.register_user_id, request.register_user_fullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()), request.update_user_id, request.update_user_fullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()), request.state);

			var result = await _iPreContractBankAccountRepository.Register(preContractBankAccount);

			return result;
		}
	}
}
