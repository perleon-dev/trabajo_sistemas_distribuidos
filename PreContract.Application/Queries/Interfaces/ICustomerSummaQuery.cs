using Contracts.Aplication.Queries.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Aplication.Queries.Interfaces
{
	public interface ICustomerSummaQuery
	{
		Task<IEnumerable<CustomerSummaViewModel>> GetBySearch(CustomerSummaRequest request);
	}
}
