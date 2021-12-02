using System.Threading.Tasks;

namespace Contracts.Api.Domain.Aggregates.LogContractAggregate
{
	public interface ILogContractRepository
	{
		Task<int> Register(LogContract logContract);
	}
}