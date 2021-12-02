using Contracts.Api.Domain.Aggregates.PreContractBankAccountAggregate;
using Contracts.Api.Domain.Aggregates.PreContractEconomicConditionAggregate;
using Contracts.Api.Domain.Aggregates.PreContractFixedCommissionRangeAggregate;
using Contracts.Api.Domain.Aggregates.PreContractTradenameAggregate;
using Contracts.Api.Domain.Aggregates.PreContractVariableCommissionRangeAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Api.Domain.Aggregates.PreContractAggregate
{
	public interface IPreContractRepository
	{
		Task<int> Register(PreContract preContract);
		Task<int> RegisterUnified(PreContract preContract, PreContractBankAccount bankAccount, List<PreContractTradename> tradenames, List<PreContractFixedCommissionRange> fixedCommissions, List<PreContractVariableCommissionRange> variableCommissions, List<PreContractEconomicCondition> economicConditions);
		Task<int> UpdateStateJson(List<PreContract> preContractList, List<PreContractTradename> tradenameList);
	}
}
