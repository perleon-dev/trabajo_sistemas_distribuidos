using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PreContracts.Api.Domain.Aggregates.PreContractTradenameAggregate
{
	public interface IPreContractTradenameRepository
	{
		Task<int> Register(PreContractTradename preContractTradename);
	}
}
