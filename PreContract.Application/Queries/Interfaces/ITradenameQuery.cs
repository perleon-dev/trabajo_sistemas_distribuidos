using Contracts.Api.Application.Queries.ViewModels;
using Contracts.Application.Queries.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Application.Queries.Interfaces
{
	public interface ITradenameQuery
	{
		Task<TradenameViewModel> GetById(int nomb_com_c_iid);
		Task<IEnumerable<TradenameViewModel>> GetBySearch(TradenameRequest request);
	}
}