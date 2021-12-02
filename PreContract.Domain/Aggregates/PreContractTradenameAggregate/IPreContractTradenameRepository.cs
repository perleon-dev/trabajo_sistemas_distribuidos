using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Api.Domain.Aggregates.PreContractTradenameAggregate
{
	public interface IPreContractTradenameRepository
	{
		Task<int> Register(PreContractTradename preContractTradename);
	}
}
