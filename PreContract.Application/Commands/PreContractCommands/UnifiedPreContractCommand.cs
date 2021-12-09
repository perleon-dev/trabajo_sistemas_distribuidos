using PreContracts.Api.Application.Commands.PreContractBankAccountCommands;
using PreContracts.Api.Application.Commands.PreContractEconomicConditionCommands;
using PreContracts.Api.Application.Commands.PreContractFixedCommissionRangeCommands;
using PreContracts.Api.Application.Commands.PreContractTradenameCommands;
using PreContracts.Api.Application.Commands.PreContractVariableCommissionRangeCommand;
using MediatR;
using System.Collections.Generic;

namespace PreContracts.Api.Application.Commands.PreContractCommands
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
