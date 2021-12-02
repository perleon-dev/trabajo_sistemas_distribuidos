using System.Threading.Tasks;

namespace Contracts.Api.Domain.Aggregates.PreContractLogAggregate
{
	public interface IPreContractLogRepository
	{
		Task<int> Register(PreContractLog preContractLog);
		Task<int> UpdateStatus(PreContractLog preContractLog);
	}
}
 