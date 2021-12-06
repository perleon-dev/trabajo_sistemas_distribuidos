
using Contracts.Aplication.Queries.ViewModel;
using System;

namespace Contracts.Aplication.Queries.Mappers
{
    public interface ICustomerMapper
    {
        CustomerViewModel MapToViewModel(dynamic queryResult);
    }

    public class CustomerMapper : ICustomerMapper
    {
        public CustomerViewModel MapToViewModel(dynamic queryResult)
        {
            var customer = new CustomerViewModel
            {
                PersonTypeId = queryResult.cli_c_btipo_pers ?? false ? "1" : "0",
                PersonType = queryResult.cli_c_btipo_pers_desc,
                DocumentTypeId = queryResult.par_det_c_iid == null ? string.Empty : Convert.ToInt32(queryResult.par_det_c_iid),
                DocumentTypeSap = queryResult.cli_c_vtip_doc_sap?.ToString(),
                DocumentType = queryResult.par_det_c_vdesc,
                DocumentId = queryResult.cli_c_vdoc_id,
                Name = queryResult.cli_c_vnomb,
                LastName = queryResult.cli_c_vapellidos,
                BusinessName = queryResult.cli_c_vraz_soc,
                CurrencyId = queryResult.cli_c_vtra_cli_sap,
                CurrencySap = queryResult.cli_c_vtra_cli_cod_sap,
                Currency = queryResult.cli_c_vtra_cli,
                NumberDeparture = queryResult.cli_c_vpartida,
                EconomicActivityId = queryResult.cli_c_vtip_act_id,
                EconomicActivitySap = queryResult.cli_c_vtip_act_sap,
                EconomicActivity = queryResult.cli_c_vtip_act,
                TaxClassId = queryResult.cli_c_vcla_imp_id,
                ToleranceGroup = queryResult.cli_c_vgrp_cli_sap,
                TaxClassSap = queryResult.cli_c_vcla_imp_sap,
                TaxClass = queryResult.cli_c_vcla_imp,
                ImputationGroupId = queryResult.cli_c_bgrupo_ibk ?? false ? "1" : "0",
                ImputationGroupSap = queryResult.cli_c_bgrupo_id,
                ImputationGroup = queryResult.cli_c_bgrupo,
                ClientSap = queryResult.cli_c_vcli_sap,
                IGVSap = queryResult.cli_c_vigv_sap,
                TaxIndicatorSap = queryResult.cli_c_vind_imp_sap,
                ActivityIdSap = queryResult.cli_c_vtip_act_sap,
                AutoDetractionSap = queryResult.cli_c_auto_detrac_sap,
                LockAutoDetractionSap = queryResult.cli_c_bloc_auto_detrac_sap,
                RegisterOfficeId = queryResult.ofi_reg_iid,
                NroPartida = queryResult.cli_c_vpartida,
                RegisterOfficeName = queryResult.ofi_reg_vnombre,
                itemName = queryResult.cli_c_vrubro,
                FlagAutoDetraction = queryResult.flag_auto_detraction
            };

            return customer;
        }
    }
}
