using PreContracts.Api.Application.Queries.ViewModels;

namespace PreContracts.Api.Application.Queries.Mappers
{
	public interface IPreContractMapper
	{
		PreContractViewModel MapToPreContractViewModel(dynamic r);
	}

	public class PreContractMapper : IPreContractMapper
	{
		public PreContractViewModel MapToPreContractViewModel(dynamic r)
		{
			PreContractViewModel o = new PreContractViewModel();

			o.contract_id = r.contract_id;
			o.contract_version = r.contract_version;
			o.contract_modification = r.contract_modification;
			o.contract_start_date = r.contract_start_date;
			o.contract_end_date = r.contract_end_date;
			o.ruc = r.ruc;
			o.bank_account = r.bank_account;
			o.cci = r.cci;
			o.code_contract = r.code_contract;
			o.type_seller = r.type_seller;
			o.distribution_type = r.distribution_type;
			o.product_commission = r.product_commission;
			o.state = r.state;
			o.active = r.active;
			o.tradename_id = r.tradename_id;
			o.mall_id = r.mall_id;
			o.bank_id = r.bank_id;
			o.type_currency = r.type_currency;
			o.bank_account_type = r.bank_account_type;
			o.segment_id = r.segment_id;
			o.origin = r.origin;
			o.ubigeo = r.ubigeo;
			o.commercial_template_id = r.commercial_template_id;
			o.register_user_id = r.register_user_id;
			o.register_user_fullname = r.register_user_fullname;
			o.register_datetime = r.register_datetime;
			o.update_user_id = r.update_user_id;
			o.update_user_fullname = r.update_user_fullname;
			o.update_datetime = r.update_datetime;
			o.business_name = r.business_name;
			o.tradename = r.tradename;
			o.commission_fixed = r.commission_fixed;
			o.commission_variable = r.commission_variable;
			o.economic_condition = r.economic_condition;

			return o;
		}
	}
}
