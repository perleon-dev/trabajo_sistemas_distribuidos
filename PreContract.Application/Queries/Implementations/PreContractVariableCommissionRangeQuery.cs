using Contracts.Api.Application.Queries.Interfaces;
using Contracts.Api.Application.Queries.Mappers;
using Contracts.Api.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Api.Application.Queries.Implementations
{
	public class PreContractVariableCommissionRangeQuery : IPreContractVariableCommissionRangeQuery
	{
		private readonly IQueryHandler _iGenericQuery;
		private readonly IPreContractVariableCommissionRangeMapper _iPreContractVariableCommissionRangeMapper;

		public PreContractVariableCommissionRangeQuery(IQueryHandler iGenericQuery, IPreContractVariableCommissionRangeMapper iPreContractVariableCommissionRangeMapper)
		{
			_iGenericQuery = iGenericQuery ?? throw new ArgumentNullException(nameof(iGenericQuery));
			_iPreContractVariableCommissionRangeMapper = iPreContractVariableCommissionRangeMapper ?? throw new ArgumentNullException(nameof(iPreContractVariableCommissionRangeMapper));
		}

		public async Task<PreContractVariableCommissionRangeViewModel> GetById(int contract_variable_com_range_id)
		{
			var parameters = new Dictionary<string, object>
			{
				{"contract_variable_com_range_id", contract_variable_com_range_id}
			};
			var parametersXml = await _iGenericQuery.BuildParametersXml(parameters);
			var result = await _iGenericQuery.Search<dynamic>(@"CONTRACT.adv_t_pre_contract_variable_commission_range_search", parametersXml, string.Empty);

			return result.Select(item => _iPreContractVariableCommissionRangeMapper.MapToPreContractVariableCommissionRangeViewModel(item)).FirstOrDefault();
		}

		public async Task<IEnumerable<PreContractVariableCommissionRangeViewModel>> GetBySearch(PreContractVariableCommissionRangeRequest request)
		{
			var parameters = new Dictionary<string, object>
			{
				{"contract_variable_com_range_id", request.contract_variable_com_range_id ?? 0},
				{"state", request.state ?? 0}
			};
			var parametersXml = await _iGenericQuery.BuildParametersXml(parameters);
			var result = await _iGenericQuery.Search<dynamic>(@"CONTRACT.adv_t_pre_contract_variable_commission_range_search", parametersXml, string.Empty);

			var items = result.Select(item => (PreContractVariableCommissionRangeViewModel)_iPreContractVariableCommissionRangeMapper.MapToPreContractVariableCommissionRangeViewModel(item));

			return items;
		}

		public async Task<(IEnumerable<PreContractVariableCommissionRangeViewModel>, int)> GetByFindAll(PreContractVariableCommissionRangeRequest request)
		{
			var parameters = new Dictionary<string, object>
			{
				{"contract_variable_com_range_id", request.contract_variable_com_range_id}
			};

			var parametersXml = await _iGenericQuery.BuildParametersXml(parameters);
			var (result, quantity) = await _iGenericQuery.FindAll<dynamic>(@"CONTRACT.adv_t_pre_contract_variable_commission_range_find_all", parametersXml, request.PageIndex, request.PageSize, request.SortProperty);

			var items = result.Select(item => (PreContractVariableCommissionRangeViewModel)_iPreContractVariableCommissionRangeMapper.MapToPreContractVariableCommissionRangeViewModel(item));

			return (items, quantity);
		}
	}
}
