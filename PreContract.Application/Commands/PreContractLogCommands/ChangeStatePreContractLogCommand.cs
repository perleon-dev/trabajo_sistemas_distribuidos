using MediatR;

namespace PreContracts.Api.Application.Commands.PreContractLogCommands
{
    public class ChangeStatePreContractLogCommand : IRequest<int>
    {
        public int log_id { get; set; }
        public int? state { get; set; }
        public int? register_user_id { get; set; }
        public string register_user_fullname { get; set; }
    }
}
 