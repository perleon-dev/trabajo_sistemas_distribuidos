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
	public class PreContractLogDetailQuery : IPreContractLogDetailQuery
	{
		private readonly IQueryHandler _iGenericQuery;
		private readonly IPreContractLogDetailMapper _iPreContractLogDetailMapper;

		public PreContractLogDetailQuery(IQueryHandler iGenericQuery, IPreContractLogDetailMapper iPreContractLogDetailMapper)
		{
			_iGenericQuery = iGenericQuery ?? throw new ArgumentNullException(nameof(iGenericQuery));
			_iPreContractLogDetailMapper = iPreContractLogDetailMapper ?? throw new ArgumentNullException(nameof(iPreContractLogDetailMapper));
		}

		public async Task<PreContractLogDetailViewModel> GetById(int log_detail_id)
		{
			var parameters = new Dictionary<string, object>
			{
				{"log_detail_id", log_detail_id}
			};
			var parametersXml = await _iGenericQuery.BuildParametersXml(parameters);
			var result = await _iGenericQuery.Search<dynamic>(@"CONTRACT.adv_t_pre_contract_log_detail_search", parametersXml, string.Empty);

			return result.Select(item => (PreContractLogDetailViewModel)_iPreContractLogDetailMapper.MapToPreContractLogDetailViewModel(item)).FirstOrDefault();
		}

		public async Task<IEnumerable<PreContractLogDetailViewModel>> GetBySearch(PreContractLogDetailRequest request)
		{
			var parameters = new Dictionary<string, object>
			{
				{"log_detail_id", request.log_detail_id ?? 0},
				{"log_id", request.log_id ?? 0},
				{"state", request.state ?? 0}
			};
			var parametersXml = await _iGenericQuery.BuildParametersXml(parameters);
			var result = await _iGenericQuery.Search<dynamic>(@"CONTRACT.adv_t_pre_contract_log_detail_search", parametersXml, string.Empty);

			var items = result.Select(item => (PreContractLogDetailViewModel)_iPreContractLogDetailMapper.MapToPreContractLogDetailViewModel(item));

			return items;
		}
	}
}
