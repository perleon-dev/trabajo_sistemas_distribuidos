using PreContracts.Api.Application.Queries.Generic;
using PreContracts.Api.Domain.Aggregates.PreContractLogAggregate;
using PreContracts.Api.Domain.Util;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PreContracts.Api.Application.Commands.PreContractLogCommands
{
    public class ChangeStatePreContractLogCommandHandler : IRequestHandler<ChangeStatePreContractLogCommand, int>
    {
        readonly IPreContractLogRepository _iPreContractLogRepository;
        readonly IValuesSettingsApi _iValuesSettingsApi;

        public ChangeStatePreContractLogCommandHandler(IPreContractLogRepository iPreContractLogRepository, IValuesSettingsApi iValuesSettingsApi)
        {
            _iPreContractLogRepository = iPreContractLogRepository;
            _iValuesSettingsApi = iValuesSettingsApi;
        }

        public async Task<int> Handle(ChangeStatePreContractLogCommand request, CancellationToken cancellationToken)
        {
            PreContractLog preContractLog = new PreContractLog(request.log_id, request.state, request.register_user_id, request.register_user_fullname, DateTime.Now.Peru(_iValuesSettingsApi.GetTimeZone()));
            return await this._iPreContractLogRepository.UpdateStatus(preContractLog);
        }
    }
} 
