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
		Task<(IEnumerable<PreContractViewModel>, int)> GetByFindAll(PreContractRequest request);
		Task<string> DownloadTemplateContractMarketPlace();
		Task<string> DownloadTemplateContractMarketPlaceVTex();
		Task<string> DownloadTemplateContractMarketPlaceVTexToVTex();
	}
}
