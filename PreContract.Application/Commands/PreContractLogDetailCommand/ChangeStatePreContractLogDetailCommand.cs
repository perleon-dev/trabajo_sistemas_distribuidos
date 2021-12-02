using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Api.Application.Commands.PreContractLogDetailCommand
{
    public class ChangeStatePreContractLogDetailCommand : IRequest<int>
    {
        public int log_detail_id { get; set; }
        public string observation { get; set; }
        public int? state { get; set; }
        public int? register_user_id { get; set; }
        public string register_user_fullname { get; set; }
    }
}
 