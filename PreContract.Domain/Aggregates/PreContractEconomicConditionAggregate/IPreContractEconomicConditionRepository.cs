using System.Threading.Tasks;

namespace Contracts.Api.Domain.Aggregates.PreContractEconomicConditionAggregate
{
	public interface IPreContractEconomicConditionRepository
	{
		Task<int> Register(PreContractEconomicCondition preContractEconomicCondition);
	}
}
