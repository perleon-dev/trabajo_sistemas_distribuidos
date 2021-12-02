using Contracts.Api.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Api.Application.Queries.Interfaces
{
	public interface IPreContractLogQuery
	{
		Task<PreContractLogViewModel> GetById(int log_id);

		Task<IEnumerable<PreContractLogViewModel>> GetBySearch(PreContractLogRequest request);

	}
}
