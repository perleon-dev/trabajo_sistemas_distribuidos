using PreContracts.Api.Application.Queries.Interfaces;
using Contracts.Aplication.Queries.Interfaces;
using Contracts.Aplication.Queries.Mappers;
using Contracts.Aplication.Queries.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Aplication.Queries.Implementations
{
    public class IdSummaQuery : IIdSummaQuery
    {

        private readonly IQueryHandler _iqueryHandler;
        private readonly IIdSummaMapper _iIdSummaMapper;

        public IdSummaQuery(IQueryHandler iqueryHandler, IIdSummaMapper iIdSummaMapper)
        {
            _iqueryHandler = iqueryHandler ?? throw new ArgumentNullException(nameof(iqueryHandler));
            _iIdSummaMapper = iIdSummaMapper ?? throw new ArgumentNullException(nameof(iIdSummaMapper));
        }

        public async Task<IEnumerable<IdSummaViewModel>> GetBySearch(IdSummaRequest request)
        {
            var parameters = new Dictionary<string, object>
            {
                {"id_summa", request.id_summa ?? 0},
                {"tradename", request.tradename ?? string.Empty},
                {"tradename_list", request.tradename_list ?? string.Empty}
            };

            var parametersXml = (await _iqueryHandler.BuildParametersXml(parameters)); // request.tradename_list
            var result = await _iqueryHandler.Search<dynamic>(@"SELLER.adv_t_id_summa_search", parametersXml, string.Empty);

            var items = result.Select(item => (IdSummaViewModel)_iIdSummaMapper.MapToIdSummaViewModel(item));

            return items;
        }
    }
}
