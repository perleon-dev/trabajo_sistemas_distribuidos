using PreContracts.Api.Application.Queries.Interfaces;
using PreContracts.Api.Application.Queries.Mappers;
using PreContracts.Api.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreContracts.Api.Application.Queries.Implementations
{
	public class LogContractQuery : ILogContractQuery
	{
		private readonly IQueryHandler _queryHandler;
		private readonly ILogContractMapper _iLogContractMapper;

		public LogContractQuery(IQueryHandler queryHandler, ILogContractMapper iLogContractMapper)
		{
			_queryHandler = queryHandler ?? throw new ArgumentNullException(nameof(queryHandler));
			_iLogContractMapper = iLogContractMapper ?? throw new ArgumentNullException(nameof(iLogContractMapper));
		}

		public async Task<LogContractViewModel> GetById(int logContractId)
		{
			var parameters = new Dictionary<string, object>
			{
				{"log_contract_id", logContractId}
			};
			var parametersXml = await _queryHandler.BuildParametersXml(parameters);

			var result = await _queryHandler.Search<dynamic>(@"CONTRACT.ADV_T_LOG_CONTRACT_search", parametersXml, string.Empty);

			return result.Select(item => (LogContractViewModel)_iLogContractMapper.MapToLogContractViewModel(item)).FirstOrDefault();
		}

		public async Task<IEnumerable<LogContractViewModel>> GetBySearch(LogContractRequest request)
		{
			try
			{
				var parameters = new Dictionary<string, object>
			{
				{"log_contract_id", request.logContractId}
			};
				var parametersXml = await _queryHandler.BuildParametersXml(parameters);

				var result = await _queryHandler.Search<dynamic>(@"CONTRACT.ADV_T_LOG_CONTRACT_search", parametersXml, string.Empty);

				var items = result.Select(item => (LogContractViewModel)_iLogContractMapper.MapToLogContractViewModel(item));

				return items;
			}
			catch (Exception e) 
			{
				return null;
			}
		}

		public async Task<(IEnumerable<LogContractViewModel>, int)> GetByFindAll(LogContractRequest request)
		{
			var parameters = new Dictionary<string, object>
			{
				{"log_contract_id", request.logContractId}
			};

			var parametersXml = await _queryHandler.BuildParametersXml(parameters);
			var (result, quantity) = await _queryHandler.FindAll<dynamic>(@"CONTRACT.ADV_T_LOG_CONTRACT_find_all", parametersXml, request.pageIndex, request.pageSize, request.sort);

			var items = result.Select(item => (LogContractViewModel)_iLogContractMapper.MapToLogContractViewModel(item));

			return (items, quantity);
		}
	}
}