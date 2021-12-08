using Contracts.Api.Application.Queries.Implementations;
using Contracts.Api.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Api.Application.Queries.Interfaces
{
	public interface IPreContractQuery
	{
		Task<PreContractViewModel> GetById(int contract_id);
		Task<IEnumerable<PreContractViewModel>> GetBySearch(PreContractRequest request);
		Task<PaginatePrecontract> GetByFindAll(PreContractRequest request);
		Task<string> DownloadTemplateContractMarketPlace();
		Task<string> DownloadTemplateContractMarketPlaceVTex();
		Task<string> DownloadTemplateContractMarketPlaceVTexToVTex();
	}
}
