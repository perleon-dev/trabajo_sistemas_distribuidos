using PreContracts.Api.Application.Commands.PreContractLogCommands;
using PreContracts.Api.Application.Commands.PreContractLogDetailCommand;
using PreContracts.Api.Application.Queries.Generic;
using PreContracts.Api.Application.Queries.Interfaces;
using PreContracts.Api.Application.Queries.ViewModels;
using PreContracts.Api.Domain.Util;
using Contracts.Aplication.Queries.Interfaces;
using Contracts.Aplication.Queries.ViewModel;
using Contracts.Application.Queries.Interfaces;
using Contracts.Application.Queries.ViewModels;
//using PreContracts.Api.Services.Interfaces;
using MediatR;
using Newtonsoft.Json;
//using Newtonsoft.Json;
using OfficeOpenXml;
using PreContract.Application.Commands.PreContractCommands;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PreContracts.Api.Application.Commands.PreContractCommands
{
    [ExcludeFromCodeCoverage]
    public class CreatePreContractMasisveCommandHandler : IRequestHandler<CreatePreContractMasisveCommand, MessageResponse>
    {
        //private readonly IS3Service _is3Service;
        private readonly IValuesSettings _ivaluesSettings;
        private readonly IMediator _imediator;
        private readonly IPreContractLogQuery _ipreContractLogQuery;
        private readonly IPreContractLogDetailQuery _ipreContractLogDetailQuery;
        private readonly IValuesSettingsApi _iValuesSettingsApi;
        private readonly ICategoryQueryHandler _iCategoryQueryHandler;
        private readonly IPreContractQuery _iPreContractQuery;
        private readonly ITradenameQuery _ITradenameQuery;
        private readonly ICustomerQueryHandler _ICustomerQueryHandler;
        private readonly IIdSummaQuery _IIdSummaQuery;
        private readonly ICustomerSummaQuery _ICustomerSummaQuery;

        public CreatePreContractMasisveCommandHandler(IValuesSettings valuesSettings,
                                  IMediator imediator,
                                  IPreContractLogQuery ipreContractLogQuery,
                                  IPreContractLogDetailQuery ipreContractLogDetailQuery,
                                  IValuesSettingsApi iValuesSettingsApi,
                                  ICategoryQueryHandler iCategoryQueryHandler,
                                  IPreContractQuery iPreContractQuery,
                                  ITradenameQuery ITradenameQuery,
                                  ICustomerQueryHandler ICustomerQueryHandler,
                                  IIdSummaQuery IIdSummaQuery,
                                  ICustomerSummaQuery ICustomerSummaQuery)
        {
            _imediator = imediator;
            _ipreContractLogQuery = ipreContractLogQuery;
            _ipreContractLogDetailQuery = ipreContractLogDetailQuery;
            _iValuesSettingsApi = iValuesSettingsApi;
            _iCategoryQueryHandler = iCategoryQueryHandler;
            _iPreContractQuery = iPreContractQuery;
            this._ivaluesSettings = valuesSettings;
            _ITradenameQuery = ITradenameQuery;
            _ICustomerQueryHandler = ICustomerQueryHandler;
            _IIdSummaQuery = IIdSummaQuery;
            _ICustomerSummaQuery = ICustomerSummaQuery;
        }
        public async Task<MessageResponse> Handle(CreatePreContractMasisveCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var resultValidation = validateFile(request.document.OpenReadStream(), request.contractType);

                if (resultValidation != null) return resultValidation;

                await RegisterLog(request);

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

        ///////////////////////////////////////


        public async Task<int> RegisterLog(CreatePreContractMasisveCommand command)
        {
            var result = 0;
            try
            {
                var objectExcel = command.document.OpenReadStream();

                var tradenameFound = await _ITradenameQuery.GetBySearch(new TradenameRequest() { });
                //var customersFound = await _ICustomerQueryHandler.GetByFiltersAsync(new Aplication.Queries.Querys.CustomerQuery { activo = 1});
                //var idSummaFound = await _IIdSummaQuery.GetBySearch(new IdSummaRequest { });
                var customerSummaFound = await _ICustomerSummaQuery.GetBySearch(new CustomerSummaRequest { });
                var categoryFound = await this._iCategoryQueryHandler.Search(new Api.Application.Queries.Querys.CategoryQuery());


                CreatePreContractLogCommand createPreContractLogCommand = null;
                using (ExcelPackage package = new ExcelPackage(objectExcel))
                {
                    var workBook = package.Workbook;
                    var workSheet = workBook.Worksheets.FirstOrDefault();
                    var start = workSheet.Dimension.Start.Row + 2; //Evita la Cabecera del archivo
                    var end = workSheet.Dimension.End.Row; //Cantidad de filas que contiene el archivo

                    var rowNumber = workSheet.Dimension.End.Row - 2; //Cantidad de filas que contiene el archivo
                    createPreContractLogCommand = new CreatePreContractLogCommand()
                    {
                        contract_type = command.contractType,
                        file_name = command.document.FileName,
                        register_user_id = 0,
                        register_user_fullname = "SISTEMA",
                        state = StatePreContractLog.Pending,
                        createPreContractLogDetailCommands = new List<CreatePreContractLogDetailCommand>()
                    };

                    if (command.contractType == ContractTypePreContract.VTex)
                        createPreContractLogCommand.createPreContractLogDetailCommands = await RegisterTemplateVTex(workSheet, start, end, customerSummaFound, categoryFound, tradenameFound);
                    else if (command.contractType == ContractTypePreContract.SellerCenter)
                        createPreContractLogCommand.createPreContractLogDetailCommands = await RegisterTemplateSellerCenter(workSheet, start, end, customerSummaFound, tradenameFound, categoryFound); //customersFound
                    else if (command.contractType == ContractTypePreContract.VTexToVTex)
                        createPreContractLogCommand.createPreContractLogDetailCommands = await RegisterTemplateVtexToVtex(workSheet, start, end, customerSummaFound, tradenameFound, categoryFound); // customersFound,

                    createPreContractLogCommand.number_record = createPreContractLogCommand.createPreContractLogDetailCommands.Count();
                }

                result = await this._imediator.Send(createPreContractLogCommand, new System.Threading.CancellationToken());

                var command_trasnlate = new TranslatePreContractCommand() { id = result };
                await this._imediator.Send(command_trasnlate);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"RegisterLog : {ex.ToString()}");
            }

            return await Task.FromResult(result);
        }

        private async Task<bool> validateExistsPreContract(string ruc, IEnumerable<PreContractViewModel> PreContractPending, IEnumerable<PreContractViewModel> PreContractRegister, IEnumerable<PreContractViewModel> PreContractAnulled)
        {
            bool value = false;
            /*Validar que el Pre Contrato no este con estado registrado*/
            if (!PreContractRegister.Where(x => x.ruc == ruc).Any())
            {
                /*Validar si el pre contrato tiene el estado pendiente*/
                var found = PreContractPending.Where(x => x.ruc == ruc).FirstOrDefault();
                if (found != null)
                {
                    /*Anular el Pre Contract, para registrar uno nuevo*/
                    UpdateMassiveStatePreContractCommand command = new UpdateMassiveStatePreContractCommand();
                    command.preContractList = new List<UpdateMassiveStatePreContract>();
                    command.preContractList.Add(new UpdateMassiveStatePreContract()
                    {
                        contractId = found.contract_id,
                        contractModification = found.contract_modification,
                        contractVersion = found.contract_version
                    });
                    command.state = StatePreContract.Anulled;
                    command.updateUserId = 0;
                    command.updateUserFullname = "SISTEMA";
                    await _imediator.Send(command, new System.Threading.CancellationToken());
                }
            }
            else
                value = true;

            return value;
        }

        private async Task<List<CreatePreContractLogDetailCommand>> RegisterTemplateVTex(ExcelWorksheet excelWorksheet, int startRow, int endRow, IEnumerable<CustomerSummaViewModel> customerSummaList, IEnumerable<CategoryViewModel> categorylist, IEnumerable<TradenameViewModel> tradenameList)
        {
            List<CreatePreContractLogDetailCommand> createPreContractLogDetail = new List<CreatePreContractLogDetailCommand>();
            CreatePreContractLogDetailCommand preContractLogDetails = null;
            var rucAnulled = new List<string>();
            var preContractFoundPending = await this._iPreContractQuery.GetBySearch(new PreContractRequest()
            {
                state = StatePreContract.Pending
            });

            var preContractFoundRegister = await this._iPreContractQuery.GetBySearch(new PreContractRequest()
            {
                state = StatePreContract.SatisfactionProcess
            });

            var preContractFoundAnulled = await this._iPreContractQuery.GetBySearch(new PreContractRequest()
            {
                state = StatePreContract.Anulled
            });

            for (int i = startRow; startRow <= endRow; startRow++)
            {

                string messageValidation = string.Empty;
                string messageValidationCategory = string.Empty;

                if (excelWorksheet.Cells[startRow, 2].Value != null && excelWorksheet.Cells[startRow, 1].Value != null)
                {
                    if (rucAnulled.Count() == 0 || !rucAnulled.Contains(excelWorksheet.Cells[startRow, 1].Value.ToString()))
                    {
                        var result = await validateExistsPreContract(excelWorksheet.Cells[startRow, 1].Value.ToString(), preContractFoundPending, preContractFoundRegister, preContractFoundAnulled);
                        if (result) continue;
                        else
                            rucAnulled.Add(excelWorksheet.Cells[startRow, 1].Value.ToString());
                    }

           
                }

                if (excelWorksheet.Cells[startRow, 1].Value != null && excelWorksheet.Cells[startRow, 2].Value != null &&
                    excelWorksheet.Cells[startRow, 3].Value != null && excelWorksheet.Cells[startRow, 4].Value != null &&
                    excelWorksheet.Cells[startRow, 5].Value != null && string.IsNullOrEmpty(messageValidation) && string.IsNullOrEmpty(messageValidationCategory))
                {
                    preContractLogDetails = new CreatePreContractLogDetailCommand()
                    {
                        document_id = excelWorksheet.Cells[startRow, 1].Value.ToString(),
                        id_summa = excelWorksheet.Cells[startRow, 2].Value.ToString(),
                        category_id = int.Parse(excelWorksheet.Cells[startRow, 3].Value.ToString()),
                        fixed_commisison_amount = decimal.Parse(excelWorksheet.Cells[startRow, 4].Value.ToString()),
                        start_date_contract = excelWorksheet.Cells[startRow, 5].Value.ToString().Replace("00:00:00", string.Empty),
                        state = StatePreContractLog.Pending,
                        register_user_id = 0,
                        register_user_fullname = "SISTEMA"
                    };
                    createPreContractLogDetail.Add(preContractLogDetails);
                }
                else
                {
                    int? category_id = null;
                    decimal? fixedCommissionAmount = null;

                    if (excelWorksheet.Cells[startRow, 3].Value != null)
                        category_id = int.Parse(excelWorksheet.Cells[startRow, 3].Value.ToString());
                    if (excelWorksheet.Cells[startRow, 4].Value != null)
                        fixedCommissionAmount = decimal.Parse(excelWorksheet.Cells[startRow, 4].Value.ToString());

                    preContractLogDetails = new CreatePreContractLogDetailCommand()
                    {
                        document_id = (excelWorksheet.Cells[startRow, 1].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 1].Value.ToString(),
                        business_name = (excelWorksheet.Cells[startRow, 2].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 2].Value.ToString(),
                        category_id = category_id,
                        fixed_commisison_amount = fixedCommissionAmount,
                        start_date_contract = (excelWorksheet.Cells[startRow, 5].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 5].Value.ToString().Replace("00:00:00", string.Empty),
                        state = StatePreContractLog.FailedProcess,
                        observation = getObservationVTex(excelWorksheet, startRow, messageValidation, messageValidationCategory),
                        register_user_id = 0,
                        register_user_fullname = "SISTEMA"
                    };
                    createPreContractLogDetail.Add(preContractLogDetails);
                }
            }
            return await Task.FromResult(createPreContractLogDetail);
        }

        private string getObservationVTex(ExcelWorksheet excelWorksheet, int startRow, string messageValidation, string messageValidationCategory)
        {
            string observation = string.Empty;

            if (excelWorksheet.Cells[startRow, 1].Value == null && string.IsNullOrEmpty(messageValidation))
                observation += "R.U.C. incorrecto,";
            else
                observation += messageValidation;

            if (excelWorksheet.Cells[startRow, 2].Value == null)
                observation += "Seller incorrecto,";
            if (excelWorksheet.Cells[startRow, 3].Value == null && string.IsNullOrEmpty(messageValidationCategory))
                observation += "Id Categoría incorrecto,";
            else
                observation += messageValidationCategory;

            if (excelWorksheet.Cells[startRow, 4].Value == null)
                observation += "Monto Comisión Fija incorrecto,";
            if (excelWorksheet.Cells[startRow, 5].Value == null)
                observation += "Fecha Contrato incorrecto,";

            if (observation.Length > 0)
                observation = observation.Substring(0, observation.Length - 1);

            return observation;
        }

        private string getObservationSellerCenter(ExcelWorksheet excelWorksheet, int startRow, string validationIdSumma, string messageValidationCustomer)
        {
            string observation = string.Empty;
            int fieldEmptyFixedCommission = 0;
            int fieldEmptyBank = 0;

            if (excelWorksheet.Cells[startRow, 1].Value == null && string.IsNullOrEmpty(messageValidationCustomer))
                observation += "R.U.C. incorrecto,";
            else
                observation += messageValidationCustomer;

            if (excelWorksheet.Cells[startRow, 2].Value == null)
                observation += "Id Summa incorrecto,";
            if (!string.IsNullOrEmpty(validationIdSumma))
                observation += validationIdSumma;
            if (excelWorksheet.Cells[startRow, 5].Value == null)
                observation += "Tipo Concepto incorrecto,";
            if (excelWorksheet.Cells[startRow, 7].Value == null)
                observation += "Vigencia incorrecta,";
            if (excelWorksheet.Cells[startRow, 9].Value == null)
                observation += "Rango de Meses incorrecto,";

            if (excelWorksheet.Cells[startRow, 10].Value == null)
                observation += "Monto Porcentaje incorrecto,";
            else
            {
                if (ValidateDecimal(excelWorksheet.Cells[startRow, 10].Value.ToString()))
                {
                    decimal commission = decimal.Parse(excelWorksheet.Cells[startRow, 10].Value.ToString());

                    if (commission < 0 || commission > 100)
                        observation += "La Porcentaje debe ser de 0 a 100,";
                }
                else
                    observation += "Monto Porcentaje incorrecto,";

            }

            if (excelWorksheet.Cells[startRow, 13].Value == null)
                observation += "Monto Comision incorrecto,";
            else
            {
                if (ValidateDecimal(excelWorksheet.Cells[startRow, 10].Value.ToString()))
                {
                    decimal commission = decimal.Parse(excelWorksheet.Cells[startRow, 13].Value.ToString());

                    if (commission < 0)
                        observation += "La comision debe ser positiva,";
                }
                else
                    observation += "Monto Comision incorrecto,";
            }


            //Validación campos comisión fíja
            if (excelWorksheet.Cells[startRow, 12].Value != null)
                fieldEmptyFixedCommission++;
            if (excelWorksheet.Cells[startRow, 13].Value != null)
                fieldEmptyFixedCommission++;

            if (fieldEmptyFixedCommission != 0 && fieldEmptyFixedCommission < 2)
                observation += "Debe completar los datos de la comisión fíja,";

            if (excelWorksheet.Cells[startRow, 14].Value == null)
                observation += "Fecha Inicio incorrecto,";

            //Validación campos Banco
            if (excelWorksheet.Cells[startRow, 15].Value != null)
                fieldEmptyBank++;
            if (excelWorksheet.Cells[startRow, 16].Value != null)
                fieldEmptyBank++;
            if (excelWorksheet.Cells[startRow, 17].Value != null)
                fieldEmptyBank++;
            if (excelWorksheet.Cells[startRow, 18].Value != null)
                fieldEmptyBank++;
            if (excelWorksheet.Cells[startRow, 19].Value != null)
                fieldEmptyBank++;

            if (fieldEmptyBank != 0 && fieldEmptyBank < 5)
                observation += "Debe completar los datos del banco,";

            if (observation.Length > 0)
                observation = observation.Substring(0, observation.Length - 1);

            return observation;
        }

        private string getObservationVtexToVtex(ExcelWorksheet excelWorksheet, int startRow, string validationIdSumma, string messageValidationCustomer)
        {
            string observation = string.Empty;
            int fieldEmptyBank = 0;

            if (excelWorksheet.Cells[startRow, 1].Value == null && string.IsNullOrEmpty(messageValidationCustomer))
                observation += "R.U.C. incorrecto,";
            else
                observation += messageValidationCustomer;

            if (excelWorksheet.Cells[startRow, 2].Value == null)
                observation += "Id Summa incorrecto,";

            if (excelWorksheet.Cells[startRow, 4].Value == null)
                observation += "Segmento incorrecto,";

            if (ValidateDateTime(excelWorksheet.Cells[startRow, 8].Value) == string.Empty)
                observation += "Fecha Inicio incorrecto,";

            if (!string.IsNullOrEmpty(validationIdSumma))
                observation += validationIdSumma;

            if (excelWorksheet.Cells[startRow, 7].Value == null)
                observation += "Monto Porcentaje incorrecto,";
            else
            {
                if (ValidateDecimal(excelWorksheet.Cells[startRow, 7].Value.ToString()))
                {
                    decimal commission = decimal.Parse(excelWorksheet.Cells[startRow, 7].Value.ToString());

                    if (commission < 0 || commission > 100)
                        observation += "La Porcentaje debe ser de 0 a 100,";
                }
                else
                    observation += "Monto Porcentaje incorrecto,";

            }

            //Validación campos Banco
            if (excelWorksheet.Cells[startRow, 9].Value != null)
                fieldEmptyBank++;
            if (excelWorksheet.Cells[startRow, 10].Value != null)
                fieldEmptyBank++;
            if (excelWorksheet.Cells[startRow, 11].Value != null)
                fieldEmptyBank++;
            if (excelWorksheet.Cells[startRow, 12].Value != null)
                fieldEmptyBank++;
            if (excelWorksheet.Cells[startRow, 13].Value != null)
                fieldEmptyBank++;

            if (fieldEmptyBank != 0 && fieldEmptyBank < 5)
                observation += "Debe completar los datos del banco,";

            if (observation.Length > 0)
                observation = observation.Substring(0, observation.Length - 1);

            return observation;
        }

        private async Task<List<CreatePreContractLogDetailCommand>> RegisterTemplateSellerCenter(ExcelWorksheet excelWorksheet, int startRow, int endRow, IEnumerable<CustomerSummaViewModel>  customerSummaList, IEnumerable<TradenameViewModel> tradenameList, IEnumerable<CategoryViewModel> categorylist) // IEnumerable<CustomerViewModel> customerList,
        {
            List<CreatePreContractLogDetailCommand> preContractLogDetailsList = new List<CreatePreContractLogDetailCommand>();
            CreatePreContractLogDetailCommand preContractLogDetails = null;
            var rucAnulled = new List<string>();
            var preContractFoundPending = await this._iPreContractQuery.GetBySearch(new PreContractRequest()
            {
                state = StatePreContract.Pending
            });

            var preContractFoundRegister = await this._iPreContractQuery.GetBySearch(new PreContractRequest()
            {
                state = StatePreContract.SatisfactionProcess
            });

            var preContractFoundAnulled = await this._iPreContractQuery.GetBySearch(new PreContractRequest()
            {
                state = StatePreContract.Anulled
            });

            for (int i = startRow; startRow <= endRow; startRow++)
            {

                if (excelWorksheet.Cells[startRow, 1].Value == null) continue;

                int? monthRangeFidexCommission = null;
                int? monthRangeCommissionVariable = null;
                decimal? percentageCommissionVariable = null;
                decimal? fixedCommisisonAmount = null;
                string messageValidationIdSumma = string.Empty;
                string messageValidationCustomer = string.Empty;

                if (excelWorksheet.Cells[startRow, 1].Value != null)
                {

                    if (rucAnulled.Count() == 0 || !rucAnulled.Contains(excelWorksheet.Cells[startRow, 1].Value.ToString()))
                    {
                        var result = await validateExistsPreContract(excelWorksheet.Cells[startRow, 1].Value.ToString(), preContractFoundPending, preContractFoundRegister, preContractFoundAnulled);
                        if (result) continue;
                        else rucAnulled.Add(excelWorksheet.Cells[startRow, 1].Value.ToString());
                    }

                    //if (!customerList.Where(c => c.DocumentId == excelWorksheet.Cells[startRow, 1].Value.ToString().Trim()).Any())
                    //    messageValidationCustomer = "El R.U.C., no esta registrado como cliente,";
                }


                if (!ValidityCategory(categorylist, excelWorksheet.Cells[startRow, 6].Value))
                    messageValidationIdSumma = "La Categoria no es valida,";

                if (!ValidateNumber(excelWorksheet.Cells[startRow, 9].Value))
                    messageValidationIdSumma += "Los meses de la columna (I) no tiene el formato numerico adecuado,";

                if (!ValidateNumber(excelWorksheet.Cells[startRow, 12].Value))
                    messageValidationIdSumma += "Los meses de la columna (L) no tiene el formato numerico adecuado,";


                if (excelWorksheet.Cells[startRow, 12].Value != null && ValidateNumber(excelWorksheet.Cells[startRow, 12].Value))
                    monthRangeFidexCommission = int.Parse(excelWorksheet.Cells[startRow, 12].Value.ToString());
                if (excelWorksheet.Cells[startRow, 13].Value != null)
                    fixedCommisisonAmount = decimal.Parse(excelWorksheet.Cells[startRow, 13].Value.ToString());
                if (excelWorksheet.Cells[startRow, 9].Value != null && ValidateNumber(excelWorksheet.Cells[startRow, 9].Value))
                    monthRangeCommissionVariable = int.Parse(excelWorksheet.Cells[startRow, 9].Value.ToString());
                if (excelWorksheet.Cells[startRow, 10].Value != null)
                    percentageCommissionVariable = decimal.Parse(excelWorksheet.Cells[startRow, 10].Value.ToString());

                if (excelWorksheet.Cells[startRow, 1].Value != null && excelWorksheet.Cells[startRow, 2].Value != null && excelWorksheet.Cells[startRow, 5].Value != null &&
                    excelWorksheet.Cells[startRow, 7].Value != null && excelWorksheet.Cells[startRow, 9].Value != null && excelWorksheet.Cells[startRow, 10].Value != null &&
                    excelWorksheet.Cells[startRow, 14].Value != null && string.IsNullOrEmpty(messageValidationIdSumma) && string.IsNullOrEmpty(messageValidationCustomer) &&
                    ValidateCommissionValidity(excelWorksheet.Cells[startRow, 10].Value, excelWorksheet.Cells[startRow, 13].Value) && ValidityCategory(categorylist, excelWorksheet.Cells[startRow, 6].Value))
                {

                    preContractLogDetails = new CreatePreContractLogDetailCommand()
                    {
                        document_id = excelWorksheet.Cells[startRow, 1].Value.ToString(),
                        id_summa = (excelWorksheet.Cells[startRow, 2].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 2].Value.ToString(),
                        item = (excelWorksheet.Cells[startRow, 3].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 3].Value.ToString(), //Rubro
                        segment = (excelWorksheet.Cells[startRow, 4].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 4].Value.ToString(), //Segmento
                        commission_variable = (excelWorksheet.Cells[startRow, 5].Value == null) ? 0 : 1, //Tipo Concepto
                        category_name = (excelWorksheet.Cells[startRow, 6].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 6].Value.ToString(), //Nombre Categoria
                        validity = (excelWorksheet.Cells[startRow, 7].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 7].Value.ToString(), //Vigencia
                        commisison_type = (excelWorksheet.Cells[startRow, 8].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 8].Value.ToString(), //Tipo comision
                        month_range_commission_variable = int.Parse(excelWorksheet.Cells[startRow, 9].Value.ToString()), //Rango meses
                        percentage_commission_variable = decimal.Parse(excelWorksheet.Cells[startRow, 10].Value.ToString()), // porcentaje
                        fixed_commission = (excelWorksheet.Cells[startRow, 11].Value == null) ? 0 : 1, //Comision fija,

                        month_range_fixed_commission = monthRangeFidexCommission, //Rango meses
                        fixed_commisison_amount = fixedCommisisonAmount, //Monto
                        start_date_contract = (excelWorksheet.Cells[startRow, 14].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 14].Value.ToString().Replace(" 00:00:00", string.Empty), //Fecha Contrato
                        bank_name = (excelWorksheet.Cells[startRow, 15].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 15].Value.ToString(), //Banco
                        bank_account = (excelWorksheet.Cells[startRow, 16].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 16].Value.ToString(), //Cuenta bancaria
                        interbank_account = (excelWorksheet.Cells[startRow, 17].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 17].Value.ToString(), //Cuenta interbancaria
                        currency_name = (excelWorksheet.Cells[startRow, 18].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 18].Value.ToString(), //Moneda,
                        bank_account_type_name = (excelWorksheet.Cells[startRow, 19].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 19].Value.ToString(), //Tipo cuenta bancaria
                        state = StatePreContractLog.Pending,
                        register_user_id = 0,
                        register_user_fullname = "SISTEMA"
                    };
                    preContractLogDetailsList.Add(preContractLogDetails);
                }
                else
                {
                    preContractLogDetails = new CreatePreContractLogDetailCommand()
                    {
                        document_id = (excelWorksheet.Cells[startRow, 1].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 1].Value.ToString(),
                        id_summa = (excelWorksheet.Cells[startRow, 2].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 2].Value.ToString(),
                        item = (excelWorksheet.Cells[startRow, 3].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 3].Value.ToString(), //Rubro
                        segment = (excelWorksheet.Cells[startRow, 4].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 4].Value.ToString(), //Segmento
                        commission_variable = (excelWorksheet.Cells[startRow, 5].Value == null) ? 0 : 1, //Tipo Concepto
                        category_name = (excelWorksheet.Cells[startRow, 6].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 6].Value.ToString(), //Nombre Categoria
                        validity = (excelWorksheet.Cells[startRow, 7].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 7].Value.ToString(), //Vigencia
                        commisison_type = (excelWorksheet.Cells[startRow, 8].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 8].Value.ToString(), //Tipo comision
                        month_range_commission_variable = monthRangeCommissionVariable, //Rango meses
                        percentage_commission_variable = percentageCommissionVariable, // porcentaje
                        fixed_commission = (excelWorksheet.Cells[startRow, 11].Value == null) ? 0 : 1, //Comision fija,

                        month_range_fixed_commission = monthRangeFidexCommission, //Rango meses
                        fixed_commisison_amount = fixedCommisisonAmount, //Monto
                        start_date_contract = (excelWorksheet.Cells[startRow, 14].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 14].Value.ToString().Replace(" 00:00:00", string.Empty), //Fecha Contrato
                        bank_name = (excelWorksheet.Cells[startRow, 15].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 15].Value.ToString(), //Banco
                        bank_account = (excelWorksheet.Cells[startRow, 16].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 16].Value.ToString(), //Cuenta bancaria
                        interbank_account = (excelWorksheet.Cells[startRow, 17].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 17].Value.ToString(), //Cuenta interbancaria
                        currency_name = (excelWorksheet.Cells[startRow, 18].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 18].Value.ToString(), //Moneda,
                        bank_account_type_name = (excelWorksheet.Cells[startRow, 19].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 19].Value.ToString(), //Tipo cuenta bancaria
                        state = StatePreContractLog.FailedProcess,
                        observation = getObservationSellerCenter(excelWorksheet, startRow, messageValidationIdSumma, messageValidationCustomer),
                        register_user_id = 0,
                        register_user_fullname = "SISTEMA"
                    };
                    preContractLogDetailsList.Add(preContractLogDetails);
                }

            }

            return await Task.FromResult(preContractLogDetailsList);
        }

        private async Task<List<CreatePreContractLogDetailCommand>> RegisterTemplateVtexToVtex(ExcelWorksheet excelWorksheet, int startRow, int endRow, IEnumerable<CustomerSummaViewModel>  customerSummaList, IEnumerable<TradenameViewModel> tradenameList, IEnumerable<CategoryViewModel> categorylist) // IEnumerable<CustomerViewModel> customerList, I
        {
            List<CreatePreContractLogDetailCommand> preContractLogDetailsList = new List<CreatePreContractLogDetailCommand>();
            CreatePreContractLogDetailCommand preContractLogDetails = null;
            var rucAnulled = new List<string>();

            var preContractFoundPending = await this._iPreContractQuery.GetBySearch(new PreContractRequest()
            {
                state = StatePreContract.Pending
            });

            var preContractFoundRegister = await this._iPreContractQuery.GetBySearch(new PreContractRequest()
            {
                state = StatePreContract.SatisfactionProcess
            });

            var preContractFoundAnulled = await this._iPreContractQuery.GetBySearch(new PreContractRequest()
            {
                state = StatePreContract.Anulled
            });

            for (int i = startRow; startRow <= endRow; startRow++)
            {

                if (excelWorksheet.Cells[startRow, 1].Value == null) continue;

                decimal? percentageCommissionVariable = null;
                int? monthRangeCommissionVariable = null;
                string messageValidation = string.Empty;
                string messageValidationCustomer = string.Empty;

                if (excelWorksheet.Cells[startRow, 1].Value != null)
                {

                    if (rucAnulled.Count() == 0 || !rucAnulled.Contains(excelWorksheet.Cells[startRow, 1].Value.ToString()))
                    {
                        var result = await validateExistsPreContract(excelWorksheet.Cells[startRow, 1].Value.ToString(), preContractFoundPending, preContractFoundRegister, preContractFoundAnulled);
                        if (result) continue;
                        else rucAnulled.Add(excelWorksheet.Cells[startRow, 1].Value.ToString());
                    }

                    //if (!customerList.Where(c => c.DocumentId == excelWorksheet.Cells[startRow, 1].Value.ToString().Trim()).Any())
                    //    messageValidationCustomer = "El R.U.C., no esta registrado como cliente,";
                }


                if (!ValidityCategory(categorylist, excelWorksheet.Cells[startRow, 5].Value))
                    messageValidation = "La Categoria no es valida,";


                if (excelWorksheet.Cells[startRow, 7].Value != null && ValidateDecimal(excelWorksheet.Cells[startRow, 7].Value.ToString()))
                    percentageCommissionVariable = decimal.Parse(excelWorksheet.Cells[startRow, 7].Value.ToString());


                if (excelWorksheet.Cells[startRow, 6].Value != null && ValidateNumber(excelWorksheet.Cells[startRow, 6].Value))
                    monthRangeCommissionVariable = int.Parse(excelWorksheet.Cells[startRow, 6].Value.ToString());

                if (excelWorksheet.Cells[startRow, 1].Value != null &&
                    excelWorksheet.Cells[startRow, 2].Value != null &&
                    excelWorksheet.Cells[startRow, 4].Value != null &&
                    !string.IsNullOrEmpty(ValidateDateTime(excelWorksheet.Cells[startRow, 8].Value)) &&
                    excelWorksheet.Cells[startRow, 9].Value != null &&
                    excelWorksheet.Cells[startRow, 10].Value != null &&
                    excelWorksheet.Cells[startRow, 11].Value != null &&
                    excelWorksheet.Cells[startRow, 12].Value != null &&
                    excelWorksheet.Cells[startRow, 13].Value != null &&
                    string.IsNullOrEmpty(messageValidation) &&
                    string.IsNullOrEmpty(messageValidationCustomer) &&
                    ValidateNumber(excelWorksheet.Cells[startRow, 6].Value) &&
                    ValidateDecimal(excelWorksheet.Cells[startRow, 7].Value.ToString()) &&
                    ((ValidityCategory(categorylist, excelWorksheet.Cells[startRow, 5].Value) &&
                    GetCategory(categorylist, excelWorksheet.Cells[startRow, 5].Value) != null) ||
                    excelWorksheet.Cells[startRow, 5].Value == null))
                {
                    var category_name = (excelWorksheet.Cells[startRow, 5].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 5].Value.ToString();

                    preContractLogDetails = new CreatePreContractLogDetailCommand()
                    {
                        document_id = excelWorksheet.Cells[startRow, 1].Value.ToString(),
                        id_summa = excelWorksheet.Cells[startRow, 2].Value.ToString(),
                        item = (excelWorksheet.Cells[startRow, 3].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 3].Value.ToString(), //Rubro
                        segment = excelWorksheet.Cells[startRow, 4].Value.ToString(), //Segmento
                        commission_variable = 1, //Tipo Concepto
                        category_name = category_name, //Nombre Categoria
                        category_id = GetCategory(categorylist, category_name),  // ID Categoria
                        month_range_commission_variable = monthRangeCommissionVariable, //Rango meses

                        percentage_commission_variable = percentageCommissionVariable, //Monto
                        start_date_contract = excelWorksheet.Cells[startRow, 8].Value.ToString().Replace(" 00:00:00", string.Empty), //Fecha Contrato
                        bank_name = excelWorksheet.Cells[startRow, 9].Value.ToString(), //Banco
                        bank_account = excelWorksheet.Cells[startRow, 10].Value.ToString(), //Cuenta bancaria
                        interbank_account = excelWorksheet.Cells[startRow, 11].Value.ToString(), //Cuenta interbancaria
                        currency_name = excelWorksheet.Cells[startRow, 12].Value.ToString(), //Moneda,
                        bank_account_type_name = excelWorksheet.Cells[startRow, 13].Value.ToString(), //Tipo cuenta bancaria
                        state = StatePreContractLog.Pending,
                        register_user_id = 0,
                        register_user_fullname = "SISTEMA"
                    };
                    preContractLogDetailsList.Add(preContractLogDetails);
                }
                else
                {
                    var category_name = (excelWorksheet.Cells[startRow, 5].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 5].Value.ToString();

                    preContractLogDetails = new CreatePreContractLogDetailCommand()
                    {
                        document_id = (excelWorksheet.Cells[startRow, 1].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 1].Value.ToString(),
                        id_summa = (excelWorksheet.Cells[startRow, 2].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 2].Value.ToString(),
                        item = (excelWorksheet.Cells[startRow, 3].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 3].Value.ToString(), //Rubro
                        segment = (excelWorksheet.Cells[startRow, 4].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 4].Value.ToString(), //Segmento
                        commission_variable = 1, //Tipo Concepto
                        category_name = category_name, //Nombre Categoria
                        category_id = GetCategory(categorylist, category_name),  // ID Categoria
                        month_range_commission_variable = monthRangeCommissionVariable, //Rango meses

                        percentage_commission_variable = percentageCommissionVariable, //Monto
                        start_date_contract = (excelWorksheet.Cells[startRow, 8].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 8].Value.ToString().Replace(" 00:00:00", string.Empty), //Fecha Contrato
                        bank_name = (excelWorksheet.Cells[startRow, 9].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 9].Value.ToString(), //Banco
                        bank_account = (excelWorksheet.Cells[startRow, 10].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 10].Value.ToString(), //Cuenta bancaria
                        interbank_account = (excelWorksheet.Cells[startRow, 11].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 11].Value.ToString(), //Cuenta interbancaria
                        currency_name = (excelWorksheet.Cells[startRow, 12].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 12].Value.ToString(), //Moneda,
                        bank_account_type_name = (excelWorksheet.Cells[startRow, 13].Value == null) ? string.Empty : excelWorksheet.Cells[startRow, 13].Value.ToString(), //Tipo cuenta bancaria
                        state = StatePreContractLog.FailedProcess,
                        observation = getObservationVtexToVtex(excelWorksheet, startRow, messageValidation, messageValidationCustomer),
                        register_user_id = 0,
                        register_user_fullname = "SISTEMA"
                    };
                    preContractLogDetailsList.Add(preContractLogDetails);
                }

            }

            return await Task.FromResult(preContractLogDetailsList);
        }


        private bool ValidityCategory(IEnumerable<CategoryViewModel> categorylist, dynamic category)
        {
            bool validity = false;
            var category_current = (category == null) ? string.Empty : category.ToString();

            if (string.IsNullOrEmpty(category_current))
            {
                validity = true;
            }
            else
            {
                string categoryID = null;

                var category_desc = category_current.Replace(" ", string.Empty).Split('-');
                categoryID = category_desc[0];

                var category_temp = categorylist.Where(x => x.categoryId.ToString() == categoryID).FirstOrDefault();

                validity = (category_temp == null) ? false : true;
            }

            return validity;
        }

        private int? GetCategory(IEnumerable<CategoryViewModel> categorylist, dynamic category)
        {
            var category_current = (category == null) ? string.Empty : category.ToString();
            int? categoryID = null;

            if (string.IsNullOrEmpty(category_current))
            {
                categoryID = null;
            }
            else
            {
                var category_desc = category_current.Replace(" ", string.Empty).Split('-');
                var category_id = Int32.Parse(category_desc[0]);

                var category_id_temp = categorylist.Where(x => x.categoryId.ToString() == category_id.ToString()).FirstOrDefault().categoryId;

                categoryID = (ValidateNumber(category_id_temp)) ? category_id_temp : (int?)null;
            }

            return categoryID;
        }

        public async Task<IEnumerable<PreContractLogViewModel>> SearchPreContractLog(string fileName, int state)
        {
            return await this._ipreContractLogQuery.GetBySearch(new PreContractLogRequest()
            {
                file_name = fileName,
                state = state
            });
        }

        public async Task<IEnumerable<PreContractLogDetailViewModel>> SearchPreContractlogDetail(int logId, int state)
        {
            return await this._ipreContractLogDetailQuery.GetBySearch(new PreContractLogDetailRequest()
            {
                log_id = logId,
                state = state
            });
        }

        private bool ValidateCommissionValidity(dynamic percentage, dynamic commission)
        {
            bool validate = true;
            if (percentage == null)
                validate = false;
            else
            {
                decimal percentage_ = decimal.Parse(percentage.ToString());

                if (percentage_ < 0 || percentage_ > 100)
                    validate = false;
            }

            if (commission == null)
                validate = false;
            else
            {
                decimal commission_ = decimal.Parse(commission.ToString());

                if (commission_ < 0)
                    validate = false;
            }
            return validate;
        }

        private bool ValidateNumber(dynamic number)
        {
            bool validate = false;
            int number_;
            if (number != null)
            {
                if (!int.TryParse(number.ToString(), out number_))
                {
                    validate = false;
                }
                else
                {
                    int num = Int32.Parse(number.ToString());
                    if (num >= 0)
                        validate = true;
                    else
                        validate = false;
                }

            }
            else
                validate = false;

            return validate;
        }

        private bool ValidateDecimal(dynamic number)
        {
            bool validate = false;
            decimal number_;
            if (number != null)
            {
                if (!decimal.TryParse(number.ToString(), out number_))
                {
                    validate = false;
                }
                else
                {
                    decimal num = decimal.Parse(number.ToString());
                    if (num >= 0)
                        validate = true;
                    else
                        validate = false;
                }

            }
            else
                validate = false;

            return validate;
        }

        private string GetContractType(int contractType)
        {
            var tipo_contrato = string.Empty;

            if (contractType == ContractTypePreContract.VTex)
                tipo_contrato = "VTex";
            else if (contractType == ContractTypePreContract.SellerCenter)
                tipo_contrato = "Seller Center";
            else if (contractType == ContractTypePreContract.VTexToVTex)
                tipo_contrato = "VTex To Vtex";

            return tipo_contrato;
        }

        private string ValidateDateTime(dynamic date)
        {
            DateTime temp;
            if (date != null)
            {
                if (DateTime.TryParse(date.ToString(), out temp))
                {
                    return date.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
