using Contracts.Api.Application.Queries.Generic;
using Contracts.Api.Application.Queries.Interfaces;
using Contracts.Api.Domain.Aggregates.PreContractAggregate;
using Contracts.Api.Domain.Aggregates.PreContractTradenameAggregate;
using Contracts.Api.Domain.Util;
//using Contracts.Api.Services.Interfaces;
using MediatR;
//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Contracts.Api.Application.Commands.PreContractCommands
{
    [ExcludeFromCodeCoverage]
    public class SendPreContractCommandHandler : IRequestHandler<SendPreContractCommand, MessageResponse>
    {
        //private readonly IS3Service _iS3Service;
        private readonly IValuesSettings _iValuesSettings;
        readonly IPreContractTradenameQuery _iPreContractTradenameQuery;
        readonly IPreContractRepository _iPreContractRepository;

        public SendPreContractCommandHandler( IValuesSettings iValuesSettings,
                                             IPreContractRepository iPreContractRepository, 
                                             IPreContractTradenameQuery iPreContractTradenameQuery) //IS3Service iS3Service,
        {
            //_iS3Service = iS3Service;
            _iValuesSettings = iValuesSettings;
            _iPreContractRepository = iPreContractRepository;
            _iPreContractTradenameQuery = iPreContractTradenameQuery;
        }

        public async Task<MessageResponse> Handle(SendPreContractCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //var nameFileJson = DateTime.Now.ToString("ddMMyyyyhhmmss");
                //var documentList = string.Empty;

                //using (var memory = new MemoryStream())
                //{
                //    var tw = new StreamWriter(memory);
                //    fileJson fileJson = new fileJson()
                //    {
                //        documentIdList = String.Join(",", request.sendPreContractList.Select(x => x.ruc).ToArray())
                //    };
                //    await tw.WriteLineAsync(JsonConvert.SerializeObject(fileJson));
                //    await tw.FlushAsync();
                //    await this._iS3Service.UploadFile(memory, nameFileJson + "-send-pre-contract.json", this._iValuesSettings.getBucketNamePreContractJson());
                //}

                //var preContractsToUpdate = new List<PreContract>();
                //var preContractTradenamesToUpdate = new List<PreContractTradename>();

                //foreach (var preContract in request.sendPreContractList)
                //    preContractsToUpdate.Add(new PreContract(preContract.contract_id, preContract.contract_version, preContract.contract_modification, request.state, request.registerUserId, request.registerFullname));

                //await _iPreContractRepository.UpdateStateJson(preContractsToUpdate, preContractTradenamesToUpdate);

                return new MessageResponse()
                {
                    codeStatus = CodeStatus.Create,
                    message = "En unos momentos se iniciará el proceso de registro de contratos"
                };
            }
            catch
            {
                return new MessageResponse()
                {
                    codeStatus = CodeStatus.InternalError,
                    message = "Ocurrió un error al intentar realizar el proceso"
                };
            }
            
        }

        class fileJson 
        {
            public string documentIdList { get; set; }
        }

    }
}
