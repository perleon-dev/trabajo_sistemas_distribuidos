using PreContracts.Api.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PreContracts.Api.Application.Queries.Interfaces
{
	public interface IPreContractVariableCommissionRangeQuery
	{
		Task<PreContractVariableCommissionRangeViewModel> GetById(int contract_variable_com_range_id);

		Task<IEnumerable<PreContractVariableCommissionRangeViewModel>> GetBySearch(PreContractVariableCommissionRangeRequest request);

		Task<(IEnumerable<PreContractVariableCommissionRangeViewModel>, int)> GetByFindAll(PreContractVariableCommissionRangeRequest request);
	}
}
