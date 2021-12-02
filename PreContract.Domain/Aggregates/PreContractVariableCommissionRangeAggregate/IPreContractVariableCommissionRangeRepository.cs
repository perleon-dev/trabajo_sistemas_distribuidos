using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Api.Domain.Aggregates.PreContractVariableCommissionRangeAggregate
{
	public interface IPreContractVariableCommissionRangeRepository
	{
		Task<int> Register(PreContractVariableCommissionRange preContractVariableCommissionRange);
	}
}
