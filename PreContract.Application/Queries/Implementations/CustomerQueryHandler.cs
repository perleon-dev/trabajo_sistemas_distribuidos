using Contracts.Api.Application.Queries.Interfaces;
using Contracts.Aplication.Queries.Interfaces;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Aplication.Queries.ViewModel;
using Contracts.Aplication.Queries.Mappers;
using Contracts.Aplication.Queries.Querys;

namespace Contracts.Aplication.Queries.Implementations
{
    [ExcludeFromCodeCoverage]
    public class CustomerQueryHandler : ICustomerQueryHandler
    {
        private readonly IQueryHandler _queryHandler;
        private readonly ICustomerMapper _customerMapper;
        
        public CustomerQueryHandler(IQueryHandler queryHandler, ICustomerMapper customerMapper)
        {
            _queryHandler = queryHandler;
            _customerMapper = customerMapper;
        }

        public async Task<IEnumerable<CustomerViewModel>> GetByFiltersAsync(CustomerQuery query)
        {
            var parameters = new Dictionary<string, object>
            {
                {"contains_cli_c_vraz_soc", query.ContainsBusinessName ?? string.Empty},
                {"cli_c_vdoc_id", query.DocumentId ?? string.Empty},
                {"cli_c_bactivo", query.activo??-1}
            };

            var parametersXml = await _queryHandler.BuildParametersXml(parameters);

            var result = await _queryHandler.Search<dynamic>("ADV_T_CLIENTE_search", parametersXml, string.Empty);

            return result.Select(item => (CustomerViewModel)_customerMapper.MapToViewModel(item));
        }

    }
}
