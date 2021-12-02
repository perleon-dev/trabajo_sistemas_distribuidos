using System.Threading.Tasks;

namespace Contracts.Api.Domain.Aggregates.PreContractBankAccountAggregate
{
	public interface IPreContractBankAccountRepository
	{
		Task<int> Register(PreContractBankAccount preContractBankAccount);
	}
}
