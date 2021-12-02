using Contracts.Api.Domain.Util;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Api.Application.Commands.PreContractCommands
{
    public class SendPreContractCommand : IRequest<MessageResponse>
    {
        public IEnumerable<SendPreContract> sendPreContractList { get; set; }
        public int state { get; set; }
        public int registerUserId { get; set; }
        public string registerFullname { get; set; }
    }

    public class SendPreContract 
    {
        public int contract_id { get; set; }
        public int contract_version { get; set; }
        public int contract_modification { get; set; }
        public string ruc { get; set; }
    }

}
