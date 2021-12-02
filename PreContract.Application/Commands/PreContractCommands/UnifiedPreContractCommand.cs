using Contracts.Api.Application.Commands.PreContractBankAccountCommands;
using Contracts.Api.Application.Commands.PreContractEconomicConditionCommands;
using Contracts.Api.Application.Commands.PreContractFixedCommissionRangeCommands;
using Contracts.Api.Application.Commands.PreContractTradenameCommands;
using Contracts.Api.Application.Commands.PreContractVariableCommissionRangeCommand;
using MediatR;
using System.Collections.Generic;

namespace Contracts.Api.Application.Commands.PreContractCommands
{
    public class UnifiedPreContractCommand : IRequest<int>
    {
        public CreatePreContractCommand preContract { get; set; }
        public CreatePreContractBankAccountCommand bankAccount { get; set; }
        public List<CreatePreContractTradenameCommand> tradenames { get; set; }
        public List<CreatePreContractFixedCommissionRangeCommand> fixedCommissions { get; set; }
        public List<CreatePreContractVariableCommissionRangeCommand> variableCommissions { get; set; }
        public List<CreatePreContractEconomicConditionCommand> economicConditions { get; set; }
    }
}
