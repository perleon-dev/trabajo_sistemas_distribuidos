using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreContract.Application.Commands.PreContractCommands
{
    public class TranslatePreContractCommand : IRequest<int>
    {
        public int id { get; set; }
    }
}
