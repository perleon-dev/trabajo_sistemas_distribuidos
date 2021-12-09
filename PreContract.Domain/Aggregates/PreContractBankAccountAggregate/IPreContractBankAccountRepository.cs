using System.Threading.Tasks;

namespace PreContracts.Api.Domain.Aggregates.PreContractBankAccountAggregate
{
	public interface IPreContractBankAccountRepository
	{
		Task<int> Register(PreContractBankAccount preContractBankAccount);
	}
}
