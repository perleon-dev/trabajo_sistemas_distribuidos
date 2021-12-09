using PreContracts.Api.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PreContracts.Api.Application.Queries.Interfaces
{
	public interface IPreContractFixedCommissionRangeQuery
	{
		Task<PreContractFixedCommissionRangeViewModel> GetById(int contract_fixed_com_range_id);

		Task<IEnumerable<PreContractFixedCommissionRangeViewModel>> GetBySearch(PreContractFixedCommissionRangeRequest request);

		Task<(IEnumerable<PreContractFixedCommissionRangeViewModel>, int)> GetByFindAll(PreContractFixedCommissionRangeRequest request);
	}
}
