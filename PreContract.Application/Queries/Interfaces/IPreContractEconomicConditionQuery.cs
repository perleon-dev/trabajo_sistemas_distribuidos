using PreContracts.Api.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PreContracts.Api.Application.Queries.Interfaces
{
	public interface IPreContractEconomicConditionQuery
	{
		Task<PreContractEconomicConditionViewModel> GetById(int economic_condition_id);

		Task<IEnumerable<PreContractEconomicConditionViewModel>> GetBySearch(PreContractEconomicConditionRequest request);
	}
}
