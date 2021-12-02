using Contracts.Api.Application.Queries.Generic;
using Contracts.Api.Domain.Aggregates.PreContractLogDetailAggregate;
using Contracts.Api.Domain.Util;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Contracts.Api.Application.Commands.PreContractLogDetailCommand
{
    public class ChangeStatePreContractLogDetailCommandHandler : IRequestHandler<ChangeStatePreContractLogDetailCommand, int>
    {
        readonly IPreContractLogDetailRepository _iPreContractLogDetailRepository;
        readonly IValuesSettingsApi _iValuesSettingsApi;
        public ChangeStatePreContractLogDetailCommandHandler(IPreContractLogDetailRepository iPreContractLogDetailRepository, IValuesSettingsApi iValuesSettingsApi)
        {
            _iPreContractLogDetailRepository = iPreContractLogDetailRepository;
            _iValuesSettingsApi = iValuesSettingsApi;
        }

        public async Task<int> Handle(ChangeStatePreContractLogDetailCommand request, CancellationToken cancellationToken)
        {
            PreContractLogDetail preContractLogDetail = new PreContractLogDetail(request.log_detail_id, request.observation, request.state, request.register_user_id, request.register_user_fullname, DateTime.Now.Peru(_iValuesSettingsApi.GetTimeZone()));
            return await this._iPreContractLogDetailRepository.UpdateState(preContractLogDetail);
        }
    }
}
 