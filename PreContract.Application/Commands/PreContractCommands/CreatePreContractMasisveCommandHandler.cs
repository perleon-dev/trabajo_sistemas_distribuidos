using Contracts.Api.Application.Queries.Generic;
using Contracts.Api.Domain.Util;
//using Contracts.Api.Services.Interfaces;
using MediatR;
//using Newtonsoft.Json;
using OfficeOpenXml;
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
    public class CreatePreContractMasisveCommandHandler : IRequestHandler<CreatePreContractMasisveCommand, MessageResponse>
    {
        //private readonly IS3Service _is3Service;
        private readonly IValuesSettings _ivaluesSettings;

        public CreatePreContractMasisveCommandHandler(IValuesSettings valuesSettings) //IS3Service s3Service, 
        {
            //this._is3Service = s3Service;
            this._ivaluesSettings = valuesSettings;
        }
        public async Task<MessageResponse> Handle(CreatePreContractMasisveCommand request, CancellationToken cancellationToken)
        {
            try
            {       
                //var resultValidation = validateFile(request.document.OpenReadStream(), request.contractType);

                //if (resultValidation != null) return resultValidation;
                //await this._is3Service.UploadFile(request.document.OpenReadStream(), request.document.FileName, this._ivaluesSettings.getBucketNamePreContractExcel());

                //var nameFileJson = DateTime.Now.ToString("ddMMyyyyhhmmss");
                //using (var memory = new MemoryStream())
                //{
                //    var tw = new StreamWriter(memory);
                //    fileJson fileJson = new fileJson()
                //    {
                //        fileName = request.document.FileName,
                //        contractType = request.contractType
                //    };
                //    await tw.WriteLineAsync(JsonConvert.SerializeObject(fileJson));
                //    await tw.FlushAsync();
                //    await this._is3Service.UploadFile(memory, nameFileJson + "-template-contract.json", this._ivaluesSettings.getBucketNamePreContractJson());
                //}

                return new MessageResponse()
                {
                    codeStatus = CodeStatus.Create,
                    message = "En unos momentos se iniciará el proceso de carga de contratos"
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

        MessageResponse validateFile(Stream stream, int contractType) 
        {
            var quantityColumn = 0;
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                var workBook = package.Workbook;
                var workSheet = workBook.Worksheets.FirstOrDefault();

                if (workSheet is null) return new MessageResponse()
                {
                    codeStatus = CodeStatus.InternalError,
                    message = "El archivo excel no tiene información"
                };

                quantityColumn = workSheet.Dimension.End.Column;
                package.Dispose();
            }

            if (contractType == ContractTypePreContract.VTex && quantityColumn != QuantityColumnsPreContract.QuantityColumnVTex)
            {
                return new MessageResponse()
                {
                    codeStatus = CodeStatus.InternalError,
                    message = "el archivo no tiene la cantidad de columnas requeridas"
                };
            }

            if (contractType == ContractTypePreContract.SellerCenter && quantityColumn != QuantityColumnsPreContract.QuantityColumnSellerCenter)
            {
                return new MessageResponse()
                {
                    codeStatus = CodeStatus.InternalError,
                    message = "el archivo no tiene la cantidad de columnas requeridas"
                };
            }

            if (contractType == ContractTypePreContract.VTexToVTex && quantityColumn != QuantityColumnsPreContract.QuantityColumnVTexToVTex)
            {
                return new MessageResponse()
                {
                    codeStatus = CodeStatus.InternalError,
                    message = "el archivo no tiene la cantidad de columnas requeridas"
                };
            }

            return null;
        }

        class fileJson
        {
            public string fileName { get; set; }
            public int contractType { get; set; }
        }

    }
}
