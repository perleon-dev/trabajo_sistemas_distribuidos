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
	public class PreContractLogQuery : IPreContractLogQuery
	{
		private readonly IQueryHandler _iGenericQuery;
		private readonly IPreContractLogMapper _iPreContractLogMapper;

		public PreContractLogQuery(IQueryHandler iGenericQuery, IPreContractLogMapper iPreContractLogMapper)
		{
			_iGenericQuery = iGenericQuery ?? throw new ArgumentNullException(nameof(iGenericQuery));
			_iPreContractLogMapper = iPreContractLogMapper ?? throw new ArgumentNullException(nameof(iPreContractLogMapper));
		}

		public async Task<PreContractLogViewModel> GetById(int log_id)
		{
			var parameters = new Dictionary<string, object>
			{
				{"log_id", log_id}
			};
			var parametersXml = await _iGenericQuery.BuildParametersXml(parameters);
			var result = await _iGenericQuery.Search<dynamic>(@"CONTRACT.adv_t_pre_contract_log_search", parametersXml, string.Empty);
			
			return result.Select(item => (PreContractLogViewModel)_iPreContractLogMapper.MapToPreContractLogViewModel(item)).FirstOrDefault();
		}

		public async Task<IEnumerable<PreContractLogViewModel>> GetBySearch(PreContractLogRequest request)
		{
			var parameters = new Dictionary<string, object>
			{
				{"log_id", request.log_id ?? 0},
				{"file_name", request.file_name ?? string.Empty},
				{"state", request.state ?? 0}
			};
			var parametersXml = await _iGenericQuery.BuildParametersXml(parameters);
			var result = await _iGenericQuery.Search<dynamic>(@"CONTRACT.adv_t_pre_contract_log_search", parametersXml, string.Empty);

			var items = result.Select(item => (PreContractLogViewModel)_iPreContractLogMapper.MapToPreContractLogViewModel(item));

			return items;
		}
	}
}
