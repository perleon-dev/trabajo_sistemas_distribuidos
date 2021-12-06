using Contracts.Api.Application.Queries.Interfaces;
using Contracts.Aplication.Queries.Interfaces;
using Contracts.Aplication.Queries.Mappers;
using Contracts.Aplication.Queries.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.Aplication.Queries.Implementations
{
    public class CustomerSummaQuery : ICustomerSummaQuery
    {
        private readonly IQueryHandler _iqueryHandler;
        private readonly ICustomerSummaMapper _iCustomerSummaMapper;

        public CustomerSummaQuery(IQueryHandler iqueryHandler, ICustomerSummaMapper iCustomerSummaMapper)
        {
            _iqueryHandler = iqueryHandler ?? throw new ArgumentNullException(nameof(iqueryHandler));
            _iCustomerSummaMapper = iCustomerSummaMapper ?? throw new ArgumentNullException(nameof(iCustomerSummaMapper));
        }

        public async Task<IEnumerable<CustomerSummaViewModel>> GetBySearch(CustomerSummaRequest request)
        {
            var parameters = new Dictionary<string, object>
            {
                {"customer_summa_id", request.customer_summa_id},
                {"document_id", request.documentId ?? string.Empty},
                {"list_document_id", request.listDocumentId ?? string.Empty},
                {"list_id_summa", request.listIdSumma ?? string.Empty}
            };
            var parametersXml = await _iqueryHandler.BuildParametersXml(parameters);
            var result = await _iqueryHandler.Search<dynamic>(@"SELLER.adv_t_customer_summa_search", parametersXml, string.Empty);

            var items = result.Select(item => (CustomerSummaViewModel)_iCustomerSummaMapper.MapToCustomerSummaViewModel(item));

            return items;
        }
    }
}
