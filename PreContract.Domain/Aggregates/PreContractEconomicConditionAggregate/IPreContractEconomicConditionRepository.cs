using System.Threading.Tasks;

namespace PreContracts.Api.Domain.Aggregates.PreContractEconomicConditionAggregate
{
	public interface IPreContractEconomicConditionRepository
	{
		Task<int> Register(PreContractEconomicCondition preContractEconomicCondition);
	}
}
