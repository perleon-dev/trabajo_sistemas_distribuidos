using MediatR;
using System.Collections.Generic;

namespace Contracts.Api.Application.Commands.PreContractLogDetailCommand
{
    public class UpdateMassivePreContractLogDetailCommand : IRequest<int>
    {
        public List<UpdatePreContractLogDetailCommand> logDetails { get; set; }
    }
}
