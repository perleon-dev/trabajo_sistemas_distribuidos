using Contracts.Aplication.Queries.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Aplication.Queries.Interfaces
{
	public interface IIdSummaQuery
	{
		Task<IEnumerable<IdSummaViewModel>> GetBySearch(IdSummaRequest request);
	}
}
