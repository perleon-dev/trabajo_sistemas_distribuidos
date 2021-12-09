using System.Threading.Tasks;

namespace PreContracts.Api.Domain.Aggregates.PreContractLogAggregate
{
	public interface IPreContractLogRepository
	{
		Task<int> Register(PreContractLog preContractLog);
		Task<int> UpdateStatus(PreContractLog preContractLog);
	}
}
 