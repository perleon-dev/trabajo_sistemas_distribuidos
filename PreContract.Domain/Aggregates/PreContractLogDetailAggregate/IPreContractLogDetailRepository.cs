using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Api.Domain.Aggregates.PreContractLogDetailAggregate
{
	public interface IPreContractLogDetailRepository
	{
		Task<int> Register(PreContractLogDetail preContractLogDetail);
		Task<int> UpdateState(PreContractLogDetail preContractLogDetail);
		Task<int> RegisterAsyncJson(List<PreContractLogDetail> preContractLogDetails);
	}
}
 