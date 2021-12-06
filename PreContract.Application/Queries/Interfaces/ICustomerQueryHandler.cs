
using Contracts.Aplication.Queries.Querys;
using Contracts.Aplication.Queries.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Aplication.Queries.Interfaces
{
    public interface ICustomerQueryHandler
    {
        Task<IEnumerable<CustomerViewModel>> GetByFiltersAsync(CustomerQuery query);
    }
}
