using PreContracts.Api.Domain.Aggregates.PreContractBankAccountAggregate;
using PreContracts.Api.Domain.Aggregates.PreContractEconomicConditionAggregate;
using PreContracts.Api.Domain.Aggregates.PreContractFixedCommissionRangeAggregate;
using PreContracts.Api.Domain.Aggregates.PreContractTradenameAggregate;
using PreContracts.Api.Domain.Aggregates.PreContractVariableCommissionRangeAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreContracts.Api.Domain.Aggregates.PreContractAggregate
{
	public interface IPreContractRepository
	{
		Task<int> Register(PreContract preContract);
		Task<int> RegisterUnified(PreContract preContract, PreContractBankAccount bankAccount, List<PreContractTradename> tradenames, List<PreContractFixedCommissionRange> fixedCommissions, List<PreContractVariableCommissionRange> variableCommissions, List<PreContractEconomicCondition> economicConditions);
		Task<int> UpdateStateJson(List<PreContract> preContractList, List<PreContractTradename> tradenameList);
	}
}
