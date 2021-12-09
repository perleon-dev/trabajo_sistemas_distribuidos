using PreContracts.Api.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PreContracts.Api.Application.Queries.Interfaces
{
	public interface IPreContractTradenameQuery
	{
		Task<PreContractTradenameViewModel> GetById(int contract_tradename_id);

		Task<IEnumerable<PreContractTradenameViewModel>> GetBySearch(PreContractTradenameRequest request);

		Task<(IEnumerable<PreContractTradenameViewModel>, int)> GetByFindAll(PreContractTradenameRequest request);
	}
}
