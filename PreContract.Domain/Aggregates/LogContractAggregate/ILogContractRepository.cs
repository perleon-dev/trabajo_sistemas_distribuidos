using System.Threading.Tasks;

namespace PreContracts.Api.Domain.Aggregates.LogContractAggregate
{
	public interface ILogContractRepository
	{
		Task<int> Register(LogContract logContract);
	}
}