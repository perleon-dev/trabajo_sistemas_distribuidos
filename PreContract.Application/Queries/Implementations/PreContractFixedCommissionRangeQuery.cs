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
	public class PreContractFixedCommissionRangeQuery : IPreContractFixedCommissionRangeQuery
	{
		private readonly IQueryHandler _iGenericQuery;
		private readonly IPreContractFixedCommissionRangeMapper _iPreContractFixedCommissionRangeMapper;

		public PreContractFixedCommissionRangeQuery(IQueryHandler iGenericQuery, IPreContractFixedCommissionRangeMapper iPreContractFixedCommissionRangeMapper)
		{
			_iGenericQuery = iGenericQuery ?? throw new ArgumentNullException(nameof(iGenericQuery));
			_iPreContractFixedCommissionRangeMapper = iPreContractFixedCommissionRangeMapper ?? throw new ArgumentNullException(nameof(iPreContractFixedCommissionRangeMapper));
		}

		public async Task<PreContractFixedCommissionRangeViewModel> GetById(int contract_fixed_com_range_id)
		{
			var parameters = new Dictionary<string, object>
			{
				{"contract_fixed_com_range_id", contract_fixed_com_range_id}
			};
			var parametersXml = await _iGenericQuery.BuildParametersXml(parameters);
			var result = await _iGenericQuery.Search<dynamic>(@"CONTRACT.adv_t_pre_contract_fixed_commission_range_search", parametersXml, string.Empty);

			return result.Select(item => _iPreContractFixedCommissionRangeMapper.MapToPreContractFixedCommissionRangeViewModel(item)).FirstOrDefault();
		}

		public async Task<IEnumerable<PreContractFixedCommissionRangeViewModel>> GetBySearch(PreContractFixedCommissionRangeRequest request)
		{
			var parameters = new Dictionary<string, object>
			{
				{"contract_fixed_com_range_id", request.contract_fixed_com_range_id ?? 0},
				{"state", request.state ?? 0}
			};
			var parametersXml = await _iGenericQuery.BuildParametersXml(parameters);
			var result = await _iGenericQuery.Search<dynamic>(@"CONTRACT.adv_t_pre_contract_fixed_commission_range_search", parametersXml, string.Empty);

			var items = result.Select(item => (PreContractFixedCommissionRangeViewModel)_iPreContractFixedCommissionRangeMapper.MapToPreContractFixedCommissionRangeViewModel(item));

			return items;
		}

		public async Task<(IEnumerable<PreContractFixedCommissionRangeViewModel>, int)> GetByFindAll(PreContractFixedCommissionRangeRequest request)
		{
			var parameters = new Dictionary<string, object>
			{
				{"contract_fixed_com_range_id", request.contract_fixed_com_range_id}
			};

			var parametersXml = await _iGenericQuery.BuildParametersXml(parameters);
			var (result, quantity) = await _iGenericQuery.FindAll<dynamic>(@"CONTRACT.adv_t_pre_contract_fixed_commission_range_find_all", parametersXml, request.PageIndex, request.PageSize, request.SortProperty);

			var items = result.Select(item => (PreContractFixedCommissionRangeViewModel)_iPreContractFixedCommissionRangeMapper.MapToPreContractFixedCommissionRangeViewModel(item));

			return (items, quantity);
		}
	}
}
