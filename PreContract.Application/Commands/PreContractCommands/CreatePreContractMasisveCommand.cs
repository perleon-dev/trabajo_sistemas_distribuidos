using PreContracts.Api.Domain.Util;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreContracts.Api.Application.Commands.PreContractCommands
{
    public class CreatePreContractMasisveCommand : IRequest<MessageResponse>
    {
        public int userId { get; set; }
        public int contractType { get; set; }
        public string userFullname { get; set; }
        public IFormFile document { get; set; }
    }
}
