using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PreContracts.Api.Domain.Aggregates.PreContractFixedCommissionRangeAggregate
{
	public interface IPreContractFixedCommissionRangeRepository
	{
		Task<int> Register(PreContractFixedCommissionRange preContractFixedCommissionRange);
	}
}
