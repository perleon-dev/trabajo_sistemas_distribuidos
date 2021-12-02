using System.Collections.Generic;
using System.Threading.Tasks;

using Contracts.Api.Application.Queries.ViewModels;

namespace Contracts.Api.Application.Queries.Interfaces
{
	public interface ILogContractQuery
	{
		Task<LogContractViewModel> GetById(int logContractId);

		Task<IEnumerable<LogContractViewModel>> GetBySearch(LogContractRequest request);

		Task<(IEnumerable<LogContractViewModel>, int)> GetByFindAll(LogContractRequest request);
	}
}