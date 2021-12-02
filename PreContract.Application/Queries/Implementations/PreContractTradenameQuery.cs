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
	public class PreContractTradenameQuery : IPreContractTradenameQuery
	{
		private readonly IQueryHandler _iGenericQuery;
		private readonly IPreContractTradenameMapper _iPreContractTradenameMapper;

		public PreContractTradenameQuery(IQueryHandler iGenericQuery, IPreContractTradenameMapper iPreContractTradenameMapper)
		{
			_iGenericQuery = iGenericQuery ?? throw new ArgumentNullException(nameof(iGenericQuery));
			_iPreContractTradenameMapper = iPreContractTradenameMapper ?? throw new ArgumentNullException(nameof(iPreContractTradenameMapper));
		}

		public async Task<PreContractTradenameViewModel> GetById(int contract_tradename_id)
		{
			var parameters = new Dictionary<string, object>
			{
				{"contract_tradename_id", contract_tradename_id}
			};

			var parametersXml = await _iGenericQuery.BuildParametersXml(parameters);
			var result = await _iGenericQuery.Search<dynamic>(@"CONTRACT.adv_t_pre_contract_tradename_search", parametersXml, string.Empty);

			return result.Select(item => (PreContractTradenameViewModel)_iPreContractTradenameMapper.MapToPreContractTradenameViewModel(item)).FirstOrDefault();
		}

		public async Task<IEnumerable<PreContractTradenameViewModel>> GetBySearch(PreContractTradenameRequest request)
		{
			var parameters = new Dictionary<string, object>
			{
				{"contract_tradename_id", request.contract_tradename_id ?? 0},
				{"state", request.state ?? 0},
				{"ruc", request.ruc ?? string.Empty}
			};
			var parametersXml = await _iGenericQuery.BuildParametersXml(parameters);
			var result = await _iGenericQuery.Search<dynamic>(@"CONTRACT.adv_t_pre_contract_tradename_search", parametersXml, string.Empty);

			var items = result.Select(item => (PreContractTradenameViewModel)_iPreContractTradenameMapper.MapToPreContractTradenameViewModel(item));

			return items;
		}

		public async Task<(IEnumerable<PreContractTradenameViewModel>, int)> GetByFindAll(PreContractTradenameRequest request)
		{
			var parameters = new Dictionary<string, object>
			{
				{"contract_tradename_id", request.contract_tradename_id}
			};
			var parametersXml = await _iGenericQuery.BuildParametersXml(parameters);
			var (result, quantity) = await _iGenericQuery.FindAll<dynamic>(@"CONTRACT.adv_t_pre_contract_tradename_find_all", parametersXml, request.PageIndex, request.PageSize, request.SortProperty);

			var items = result.Select(item => (PreContractTradenameViewModel)_iPreContractTradenameMapper.MapToPreContractTradenameViewModel(item));

			return (items, quantity);
		}
	}
}
