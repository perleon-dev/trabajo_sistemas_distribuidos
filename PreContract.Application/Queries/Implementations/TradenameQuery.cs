using Contracts.Api.Application.Queries.Interfaces;
using Contracts.Api.Application.Queries.ViewModels;
using Contracts.Application.Queries.Interfaces;
using Contracts.Application.Queries.Mappers;
using Contracts.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.Application.Queries.Implementations
{
	public class TradenameQuery : ITradenameQuery
	{
		private readonly IQueryHandler _iGenericQuery;
		private readonly ITradenameMapper _iTradenameMapper;

		public TradenameQuery(IQueryHandler iGenericQuery, ITradenameMapper iTradenameMapper)
		{
			_iGenericQuery = iGenericQuery ?? throw new ArgumentNullException(nameof(iGenericQuery));
			_iTradenameMapper = iTradenameMapper ?? throw new ArgumentNullException(nameof(iTradenameMapper));
		}

		public async Task<TradenameViewModel> GetById(int TradenameId)
		{
			var parameters = new Dictionary<string, object>
			{
				{"nomb_com_c_iid", TradenameId}
			};

			var parametersXml = await _iGenericQuery.BuildParametersXml(parameters);
			var result = await _iGenericQuery.Search<dynamic>(@"dbo.ADV_T_NOMB_COM_search", parametersXml, string.Empty);


			return (result != null) ? _iTradenameMapper.MapToTradenameViewModel(result) : null;
		}

		public async Task<IEnumerable<TradenameViewModel>> GetBySearch(TradenameRequest request)
		{
			var parameters = new Dictionary<string, object>
			{
				{"nomb_com_c_iid", request.TradenameId ?? 0},
				{"nomb_com_c_vnomb", request.tradeName ?? ""}
			};

			var parametersXml = await _iGenericQuery.BuildParametersXml(parameters);
			var result = await _iGenericQuery.Search<dynamic>(@"dbo.ADV_T_NOMB_COM_search", parametersXml, string.Empty);

			var items = result.Select(item => (TradenameViewModel)_iTradenameMapper.MapToTradenameViewModel(item));

			return items;
		}

    }
}