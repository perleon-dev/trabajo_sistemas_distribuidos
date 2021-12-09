using PreContracts.Api.Application.Commands.PreContractBankAccountCommands;
using PreContracts.Api.Application.Commands.PreContractCommands;
using PreContracts.Api.Application.Commands.PreContractEconomicConditionCommands;
using PreContracts.Api.Application.Commands.PreContractFixedCommissionRangeCommands;
using PreContracts.Api.Application.Commands.PreContractLogCommands;
using PreContracts.Api.Application.Commands.PreContractLogDetailCommand;
using PreContracts.Api.Application.Commands.PreContractTradenameCommands;
using PreContracts.Api.Application.Commands.PreContractVariableCommissionRangeCommand;
using PreContracts.Api.Application.Queries.Interfaces;
using PreContracts.Api.Application.Queries.ViewModels;
using PreContracts.Api.Domain.Util;
using Contracts.Aplication.Queries.Interfaces;
using Contracts.Aplication.Queries.ViewModel;
using Contracts.Application.Queries.Interfaces;
using Contracts.Application.Queries.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PreContract.Application.Commands.PreContractCommands
{
    public class TranslatePreContractCommandHandler : IRequestHandler<TranslatePreContractCommand, int>
    {
        private readonly IMediator _imediator;
        private readonly IPreContractLogQuery _ipreContractLogQuery;
        private readonly IPreContractLogDetailQuery _ipreContractLogDetailQuery;
        private readonly ICategoryQueryHandler _iCategoryQueryHandler;
        private readonly IPreContractQuery _iPreContractQuery;

        private readonly IIdSummaQuery _iIdSummaQuery;
        readonly ITradenameQuery _iTradenameQuery;

        public TranslatePreContractCommandHandler(IMediator imediator,
                                  IPreContractLogQuery ipreContractLogQuery,
                                  IPreContractLogDetailQuery ipreContractLogDetailQuery,
                                  ICategoryQueryHandler iCategoryQueryHandler,
                                  IPreContractQuery iPreContractQuery,
                                  IIdSummaQuery iIdSummaQuery, 
                                  ITradenameQuery iTradenameQuery)

        {
            _imediator = imediator;
            _ipreContractLogQuery = ipreContractLogQuery;
            _ipreContractLogDetailQuery = ipreContractLogDetailQuery;
            _iCategoryQueryHandler = iCategoryQueryHandler;
            _iPreContractQuery = iPreContractQuery;
            _iIdSummaQuery  = iIdSummaQuery;
            _iTradenameQuery = iTradenameQuery;
        }


        public async Task<int> Handle(TranslatePreContractCommand request, CancellationToken cancellationToken)
        {
            var id = await RegisterPreContract(request.id);
            return id;
        }

        public async Task<int> RegisterPreContract(int logPreContractId)
        {

            var logCab = await _ipreContractLogQuery.GetById(logPreContractId);

            if (logCab != null)
            {

                var categories = await _iCategoryQueryHandler.Search(new PreContracts.Api.Application.Queries.Querys.CategoryQuery { });
                var tradenames = await _iTradenameQuery.GetBySearch(new Contracts.Application.Queries.ViewModels.TradenameRequest {});
                var idsSumma = await _iIdSummaQuery.GetBySearch( new Contracts.Aplication.Queries.ViewModel.IdSummaRequest { });


                var logDetails = (await _ipreContractLogDetailQuery.GetBySearch(new PreContractLogDetailRequest() { log_id = logPreContractId })).ToList();
                var documentsId = logDetails.Select(x => x.document_id).Distinct();
                int stateLogCab = StatePreContractLog.SatisfactionProcess;

                foreach (var documentId in documentsId)
                {
                    var errors = new List<UpdatePreContractLogDetailCommand>();

                    try
                    {

                        var customerLogDetails = logDetails.Where(x => x.document_id == documentId);
                        var existsError = customerLogDetails.Where(x => x.state == StatePreContractLog.FailedProcess).Any();

                        if (existsError)
                        {
                            stateLogCab = StatePreContractLog.FailedProcess;
                            continue;
                        }

                        var contractData = customerLogDetails.FirstOrDefault();

                            var unifiedPreContractCommand = new UnifiedPreContractCommand();

                            SetPreContract(unifiedPreContractCommand, contractData, documentId, logCab, errors);

                            if (unifiedPreContractCommand.preContract == null)
                                continue;

                            SetBankAccounts(unifiedPreContractCommand, logDetails);
                            SetTradenames(unifiedPreContractCommand, logDetails, tradenames, idsSumma);

                            if (logCab.contract_type == ContractTypePreContract.SellerCenter)
                            {
                                SetFixedCommissions(unifiedPreContractCommand, logDetails);
                                SetVariableCommissions(unifiedPreContractCommand, logDetails, categories, errors);
                                SetEndDatePreContractSellerCenter(unifiedPreContractCommand);
                            }
                            else if (logCab.contract_type == ContractTypePreContract.VTexToVTex)
                            {
                                SetVariableCommissions(unifiedPreContractCommand, logDetails, categories, errors);
                                SetEndDatePreContractVTexToVTex(unifiedPreContractCommand);
                            }
                            else
                                SetEconomicConditions(unifiedPreContractCommand, logDetails, categories, errors);

                            SetProductCommission(unifiedPreContractCommand);

                            if (!errors.Any())
                            {
                                
                                await _imediator.Send(unifiedPreContractCommand);
                            }
                            else
                                stateLogCab = StatePreContractLog.FailedProcess;
    

                        Console.WriteLine("Actualizar estado de logs");
                        await UpdateLogDetails(customerLogDetails, errors);
                    }
                    catch (Exception ex)
                    {
                        stateLogCab = StatePreContractLog.FailedProcess;
                        Console.WriteLine(ex.ToString());
                    }
                }

                await UpdateLogCab(logCab, stateLogCab);
            }

            return 1;
        }

        private void SetPreContract(UnifiedPreContractCommand unifiedPreContractCommand, PreContractLogDetailViewModel contractData, string documentId, PreContractLogViewModel logCab, List<UpdatePreContractLogDetailCommand> errors)
        {

            var createPreContractCommand = new CreatePreContractCommand
            {
                active = true,
                ruc = documentId,
                register_user_id = 1,
                register_user_fullname = "SISTEMA",
                type_seller = logCab.contract_type,
                contract_start_date = Convert.ToDateTime(contractData.start_date_contract),
                state = StatePreContract.Pending,
                mall_id = Malls.RpGo,
                segment_id = null
            };

            unifiedPreContractCommand.preContract = createPreContractCommand;
        }

        private void SetBankAccounts(UnifiedPreContractCommand unifiedPreContractCommand, IEnumerable<PreContractLogDetailViewModel> logDetails)
        {
            string documentId = unifiedPreContractCommand.preContract.ruc;
            var bankAccount = logDetails.Where(x => x.document_id == documentId).FirstOrDefault();
           
           var logDetail = logDetails.Where(x => x.log_detail_id == bankAccount.log_detail_id).First();

            var createPreContractBankAccountCommand = new CreatePreContractBankAccountCommand
            {
                bank_id = null,
                account_number = bankAccount.bank_account,
                currency_id =  null,
                cci_account_number = bankAccount.interbank_account,
                type_account = null,
                register_user_id = unifiedPreContractCommand.preContract.register_user_id,
                register_user_fullname = unifiedPreContractCommand.preContract.register_user_fullname,
                state = State.Active
            };

            string bankAccountType = null, bankId = null;
            if (createPreContractBankAccountCommand.type_account.HasValue)
                bankAccountType = createPreContractBankAccountCommand.type_account.ToString();
            if (createPreContractBankAccountCommand.bank_id.HasValue)
                bankId = createPreContractBankAccountCommand.bank_id.ToString();

            unifiedPreContractCommand.preContract.bank_account = createPreContractBankAccountCommand.account_number;
            unifiedPreContractCommand.preContract.bank_account_type = bankAccountType;
            unifiedPreContractCommand.preContract.bank_id = bankId;
            unifiedPreContractCommand.preContract.cci = createPreContractBankAccountCommand.cci_account_number;

            unifiedPreContractCommand.bankAccount = createPreContractBankAccountCommand;
        }

        private void SetTradenames(UnifiedPreContractCommand unifiedPreContractCommand, IEnumerable<PreContractLogDetailViewModel> logDetails, IEnumerable<TradenameViewModel> tradenamesAdvance, IEnumerable<IdSummaViewModel> idsSumma)
        {
            string documentId = unifiedPreContractCommand.preContract.ruc;
            var tradenames = logDetails.Where(x => x.document_id == documentId).Select(x => new { x.id_summa, x.item }).Distinct();

            var tradenameCommands = new List<CreatePreContractTradenameCommand>();
            foreach (var tradename in tradenames)
            {
                var tradenameAdvance = tradenamesAdvance.Where(x => x.TradenameName != null && tradename.id_summa != null && x.TradenameName.ToLower() == tradename.id_summa.ToLower()).FirstOrDefault();
                var idSumma = idsSumma.Where(x => x.tradename != null && tradename.id_summa != null && x.tradename.ToLower() == tradename.id_summa.ToLower()).FirstOrDefault();

                if (tradenameAdvance != null && idSumma != null)
                {

                    var createPreContractTradenameCommand = new CreatePreContractTradenameCommand
                    {
                        tradename_id = tradenameAdvance.TradenameId,
                        id_summa = idSumma.tradename,
                        rubric_id = (int?)null,
                        state = State.Active,
                        register_user_id = unifiedPreContractCommand.preContract.register_user_id,
                        register_user_fullname = unifiedPreContractCommand.preContract.register_user_fullname
                    };

                    tradenameCommands.Add(createPreContractTradenameCommand);
                }
            }

            unifiedPreContractCommand.tradenames = tradenameCommands;

            if (tradenameCommands.Any())
                unifiedPreContractCommand.preContract.tradename_id = tradenameCommands.First().tradename_id;
        }

        private void SetFixedCommissions(UnifiedPreContractCommand unifiedPreContractCommand, IEnumerable<PreContractLogDetailViewModel> logDetails)
        {
            string documentId = unifiedPreContractCommand.preContract.ruc;
            DateTime validityStartDate = unifiedPreContractCommand.preContract.contract_start_date.Value;

            var fixedCommissions = logDetails.Where(x => x.document_id == documentId && x.fixed_commission == 1).Select(x => new { x.fixed_commisison_amount, x.month_range_fixed_commission, x.commisison_type }).Distinct();
            var fixedCommissionCommands = new List<CreatePreContractFixedCommissionRangeCommand>();

            foreach (var fixedCommission in fixedCommissions)
            {
                if (!fixedCommission.fixed_commisison_amount.HasValue &&
                    !fixedCommission.month_range_fixed_commission.HasValue &&
                    string.IsNullOrEmpty(fixedCommission.commisison_type))
                    continue;

                var validityEndDate = (fixedCommission.month_range_fixed_commission == 0 || !fixedCommission.month_range_fixed_commission.HasValue) ? (DateTime?)null : validityStartDate.AddMonths(fixedCommission.month_range_fixed_commission.Value).AddDays(-1);

                var createPreContractFixedCommissionRangeCommand = new CreatePreContractFixedCommissionRangeCommand
                {
                    amount = fixedCommission.fixed_commisison_amount ?? 0,
                    grade = fixedCommissionCommands.Count() + 1,
                    state = State.Active,
                    validity_start_date = validityStartDate,
                    validity_end_date = validityEndDate,
                    validity_time = fixedCommission.month_range_fixed_commission ?? 0,
                    register_user_id = unifiedPreContractCommand.preContract.register_user_id ?? 0,
                    register_user_fullname = unifiedPreContractCommand.preContract.register_user_fullname
                };

                fixedCommissionCommands.Add(createPreContractFixedCommissionRangeCommand);

                if (!validityEndDate.HasValue)
                    break;

                validityStartDate = new DateTime(validityEndDate.Value.Year, validityEndDate.Value.Month, validityEndDate.Value.Day).AddDays(1);
            }

            unifiedPreContractCommand.fixedCommissions = fixedCommissionCommands;
        }

        private void SetVariableCommissions(UnifiedPreContractCommand unifiedPreContractCommand, IEnumerable<PreContractLogDetailViewModel> logDetails, IEnumerable<CategoryViewModel> categories, List<UpdatePreContractLogDetailCommand> errors)
        {
            string documentId = unifiedPreContractCommand.preContract.ruc;
            DateTime contractStartDate = unifiedPreContractCommand.preContract.contract_start_date.Value;

            var variableCommissions = logDetails.Where(x => x.document_id == documentId && x.commission_variable == 1).Select(x => new { x.month_range_commission_variable, x.percentage_commission_variable, x.category_name, x.commisison_type, x.log_detail_id }).Distinct();
            var commissionsGroupByCategory = variableCommissions.GroupBy(x => new { x.category_name }).ToDictionary(x => x);
            var variableCommissionCommands = new List<CreatePreContractVariableCommissionRangeCommand>();

            foreach (var commissionCategory in commissionsGroupByCategory.Keys)
            {
                var initialDate = new DateTime(contractStartDate.Year, contractStartDate.Month, contractStartDate.Day);

                var commissionList = variableCommissions.Where(x => x.category_name == commissionCategory.Key.category_name).ToList();

                foreach (var variableCommission in commissionList)
                {
                    if (!variableCommission.month_range_commission_variable.HasValue &&
                    !variableCommission.percentage_commission_variable.HasValue &&
                    string.IsNullOrEmpty(variableCommission.category_name))
                        continue;

                    int monthRange = (variableCommission.month_range_commission_variable == 0 || !variableCommission.month_range_commission_variable.HasValue) ? 0 : variableCommission.month_range_commission_variable.Value;
                    var validityEndDate = (monthRange == 0) ? (DateTime?)null : initialDate.AddMonths(monthRange).AddDays(-1);

                    string categoryID = null;
                    if (!string.IsNullOrEmpty(variableCommission.category_name))
                    {
                        var desc = variableCommission.category_name.Replace(" ", string.Empty).Split('-');
                        categoryID = desc[0];
                    }

                    var category = categories.Where(x => variableCommission.category_name != null && x.categoryId.ToString() == categoryID).FirstOrDefault();
                    var logDetail = logDetails.Where(x => x.log_detail_id == variableCommission.log_detail_id).First();

                    int grade;

                    if (category != null)
                    {
                        grade = variableCommissionCommands.Where(x => x.category_id.HasValue && x.category_id == category.categoryId).Count() + 1;
                    }
                    else
                        grade = variableCommissionCommands.Where(x => !x.category_id.HasValue).Count() + 1;

                    var createPreContractVariableCommissionRangeCommand = new CreatePreContractVariableCommissionRangeCommand
                    {
                        category_id = category == null ? (int?)null : category.categoryId,
                        grade = grade,
                        state = State.Active,
                        percentage = variableCommission.percentage_commission_variable ?? 0,
                        validity_start_date = initialDate,
                        validity_end_date = validityEndDate,
                        validity_time = variableCommission.month_range_commission_variable ?? 0,
                        register_user_id = unifiedPreContractCommand.preContract.register_user_id,
                        register_user_fullname = unifiedPreContractCommand.preContract.register_user_fullname
                    };

                    variableCommissionCommands.Add(createPreContractVariableCommissionRangeCommand);

                    if (!validityEndDate.HasValue)
                        break;

                    initialDate = new DateTime(validityEndDate.Value.Year, validityEndDate.Value.Month, validityEndDate.Value.Day).AddDays(1);
                }
            }

            unifiedPreContractCommand.variableCommissions = new List<CreatePreContractVariableCommissionRangeCommand>();

            foreach (var tradename in unifiedPreContractCommand.tradenames)
            {
                foreach (var commission in variableCommissionCommands)
                {
                    unifiedPreContractCommand.variableCommissions.Add(new CreatePreContractVariableCommissionRangeCommand
                    {
                        category_id = commission.category_id,
                        contract_tradename_id = tradename.tradename_id ?? 0,
                        contract_id = commission.contract_id,
                        contract_modification = commission.contract_modification,
                        contract_version = commission.contract_version,
                        grade = commission.grade,
                        percentage = commission.percentage,
                        register_user_fullname = commission.register_user_fullname,
                        register_user_id = commission.register_user_id,
                        state = commission.state,
                        update_user_fullname = commission.update_user_fullname,
                        update_user_id = commission.update_user_id,
                        validity_active = commission.validity_active,
                        validity_end_date = commission.validity_end_date,
                        validity_start_date = commission.validity_start_date,
                        validity_time = commission.validity_time
                    });
                }
            }
        }

        private void SetEndDatePreContractSellerCenter(UnifiedPreContractCommand unifiedPreContractCommand)
        {
            var maxDateFixedCommission = unifiedPreContractCommand.fixedCommissions.Max(x => x.validity_end_date);
            var maxDateVariableCommission = unifiedPreContractCommand.variableCommissions.Max(x => x.validity_end_date);

            DateTime? precontractEndDate;
            if (!maxDateFixedCommission.HasValue || !maxDateVariableCommission.HasValue)
                precontractEndDate = null;
            else
                precontractEndDate = maxDateFixedCommission > maxDateVariableCommission ? maxDateFixedCommission : maxDateVariableCommission;

            unifiedPreContractCommand.preContract.contract_end_date = precontractEndDate;
        }

        private void SetEndDatePreContractVTexToVTex(UnifiedPreContractCommand unifiedPreContractCommand)
        {
            var maxDateVariableCommission = unifiedPreContractCommand.variableCommissions.Max(x => x.validity_end_date);

            DateTime? precontractEndDate;
            if (!maxDateVariableCommission.HasValue)
                precontractEndDate = null;
            else
                precontractEndDate = maxDateVariableCommission;

            unifiedPreContractCommand.preContract.contract_end_date = precontractEndDate;
        }

        private void SetEconomicConditions(UnifiedPreContractCommand unifiedPreContractCommand, IEnumerable<PreContractLogDetailViewModel> logDetails, IEnumerable<CategoryViewModel> categories, List<UpdatePreContractLogDetailCommand> errors)
        {
            string documentId = unifiedPreContractCommand.preContract.ruc;
            DateTime contractStartDate = unifiedPreContractCommand.preContract.contract_start_date.Value;

            var economicConditions = logDetails.Where(x => x.document_id == documentId).Select(x => new { x.category_id, x.fixed_commisison_amount, x.log_detail_id }).Distinct();
            var economicConditionCommands = new List<CreatePreContractEconomicConditionCommand>();

            foreach (var economicCondition in economicConditions)
            {
                if (!economicCondition.fixed_commisison_amount.HasValue &&
                    economicCondition.category_id == null)
                    continue;

                var logDetail = logDetails.Where(x => x.log_detail_id == economicCondition.log_detail_id).First();

                var createPreContractVariableCommissionRangeCommand = new CreatePreContractEconomicConditionCommand
                {
                    category_id = economicCondition.category_id,
                    state = State.Active,
                    commission = economicCondition.fixed_commisison_amount,
                    register_user_id = unifiedPreContractCommand.preContract.register_user_id,
                    register_user_fullname = unifiedPreContractCommand.preContract.register_user_fullname
                };

                economicConditionCommands.Add(createPreContractVariableCommissionRangeCommand);
            }

            unifiedPreContractCommand.economicConditions = economicConditionCommands;
        }

        private void SetProductCommission(UnifiedPreContractCommand unifiedPreContractCommand)
        {
            if (unifiedPreContractCommand.preContract.type_seller == ContractTypePreContract.SellerCenter ||
                unifiedPreContractCommand.preContract.type_seller == ContractTypePreContract.VTexToVTex)
            {
                var firstComission = unifiedPreContractCommand.variableCommissions.Where(x => x.state == State.Active).OrderBy(x => x.grade).FirstOrDefault();

                if (firstComission != null)
                    unifiedPreContractCommand.preContract.product_commission = firstComission.percentage;
                else
                    unifiedPreContractCommand.preContract.product_commission = 0;
            }
        }

        private UpdatePreContractLogDetailCommand GetUpdateLogDetailCommand(PreContractLogDetailViewModel logDetail, int state, string observation)
        {
            return new UpdatePreContractLogDetailCommand
            {
                log_detail_id = logDetail.log_detail_id,
                bank_account = logDetail.bank_account,
                bank_account_type_name = logDetail.bank_account_type_name,
                bank_name = logDetail.bank_name,
                business_name = logDetail.business_name,
                category_id = logDetail.category_id,
                category_name = logDetail.category_name,
                commisison_type = logDetail.commisison_type,
                commission_variable = logDetail.commission_variable,
                currency_name = logDetail.currency_name,
                document_id = logDetail.document_id,
                fixed_commisison_amount = logDetail.fixed_commisison_amount,
                fixed_commission = logDetail.fixed_commission,
                id_summa = logDetail.id_summa,
                interbank_account = logDetail.interbank_account,
                item = logDetail.item,
                log_id = logDetail.log_id,
                month_range_commission_variable = logDetail.month_range_commission_variable,
                month_range_fixed_commission = logDetail.month_range_fixed_commission,
                percentage_commission_variable = logDetail.percentage_commission_variable,
                register_user_fullname = logDetail.register_user_fullname,
                register_user_id = logDetail.register_user_id,
                segment = logDetail.segment,
                start_date_contract = logDetail.start_date_contract,
                state = state,
                observation = observation,
                validity = logDetail.validity
            };
        }

        private async Task UpdateLogDetails(IEnumerable<PreContractLogDetailViewModel> customerLogDetails, List<UpdatePreContractLogDetailCommand> errors)
        {
            var logDetailCommands = new List<UpdatePreContractLogDetailCommand>();

            foreach (var customerLogDetail in customerLogDetails)
            {
                var errorList = errors.Where(x => x.log_detail_id == customerLogDetail.log_detail_id);

                if (errorList.Any())
                {
                    var observations = String.Join("", errorList.Select(x => x.observation));
                    logDetailCommands.Add(GetUpdateLogDetailCommand(customerLogDetail, StatePreContractLog.FailedProcess, observations));
                }
                else
                    logDetailCommands.Add(GetUpdateLogDetailCommand(customerLogDetail, StatePreContractLog.SatisfactionProcess, null));
            }

            var updateMassivePreContractLogDetailCommand = new UpdateMassivePreContractLogDetailCommand()
            {
                logDetails = logDetailCommands
            };

            await _imediator.Send(updateMassivePreContractLogDetailCommand);
        }

        private async Task UpdateLogCab(PreContractLogViewModel logCab, int state)
        {
            var updatePreContractLogCommand = new UpdatePreContractLogCommand
            {
                contract_type = logCab.contract_type,
                log_id = logCab.log_id,
                file_name = logCab.file_name,
                number_record = logCab.number_record,
                register_user_fullname = logCab.register_user_fullname,
                register_user_id = logCab.register_user_id,
                state = state
            };

            await _imediator.Send(updatePreContractLogCommand);
        }
    }
}
