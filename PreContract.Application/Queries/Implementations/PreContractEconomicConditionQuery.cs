using PreContracts.Api.Application.Queries.Interfaces;
using PreContracts.Api.Application.Queries.Mappers;
using PreContracts.Api.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreContracts.Api.Application.Queries.Implementations
{
	public class PreContractEconomicConditionQuery : IPreContractEconomicConditionQuery
	{
		private readonly IQueryHandler _iGenericQuery;
		private readonly IPreContractEconomicConditionMapper _iPreContractEconomicConditionMapper;

		public PreContractEconomicConditionQuery(IQueryHandler iGenericQuery, IPreContractEconomicConditionMapper iPreContractEconomicConditionMapper)
		{
			_iGenericQuery = iGenericQuery ?? throw new ArgumentNullException(nameof(iGenericQuery));
			_iPreContractEconomicConditionMapper = iPreContractEconomicConditionMapper ?? throw new ArgumentNullException(nameof(iPreContractEconomicConditionMapper));
		}

		public async Task<PreContractEconomicConditionViewModel> GetById(int economic_condition_id)
		{
			var parameters = new Dictionary<string, object>
			{
				{"economic_condition_id", economic_condition_id}
			};
			var parametersXml = await _iGenericQuery.BuildParametersXml(parameters);
			var result = await _iGenericQuery.Search<dynamic>(@"CONTRACT.adv_t_pre_contract_economic_condition_search", parametersXml, string.Empty);

			return result.Select(item => (PreContractEconomicConditionViewModel)_iPreContractEconomicConditionMapper.MapToPreContractEconomicConditionViewModel(item)).FirstOrDefault();
		}

		public async Task<IEnumerable<PreContractEconomicConditionViewModel>> GetBySearch(PreContractEconomicConditionRequest request)
		{
			var parameters = new Dictionary<string, object>
			{
				{"economic_condition_id", request.economic_condition_id ?? 0},
				{"state", request.state ?? 0}
			};
			var parametersXml = await _iGenericQuery.BuildParametersXml(parameters);
			var result = await _iGenericQuery.Search<dynamic>(@"CONTRACT.adv_t_pre_contract_economic_condition_search", parametersXml, string.Empty);

			var items = result.Select(item => (PreContractEconomicConditionViewModel)_iPreContractEconomicConditionMapper.MapToPreContractEconomicConditionViewModel(item));

			return items;
		}
	}
}
