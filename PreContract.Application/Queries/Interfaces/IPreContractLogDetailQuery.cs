using PreContracts.Api.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PreContracts.Api.Application.Queries.Interfaces
{
	public interface IPreContractLogDetailQuery
	{
		Task<PreContractLogDetailViewModel> GetById(int log_detail_id);

		Task<IEnumerable<PreContractLogDetailViewModel>> GetBySearch(PreContractLogDetailRequest request);
	}
}
