using Contracts.Api.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Api.Application.Queries.Interfaces
{
	public interface IPreContractBankAccountQuery
	{
		Task<PreContractBankAccountViewModel> GetById(int contract_bank_account_id);

		Task<IEnumerable<PreContractBankAccountViewModel>> GetBySearch(PreContractBankAccountRequest request);

		Task<(IEnumerable<PreContractBankAccountViewModel>, int)> GetByFindAll(PreContractBankAccountRequest request);
	}
}
