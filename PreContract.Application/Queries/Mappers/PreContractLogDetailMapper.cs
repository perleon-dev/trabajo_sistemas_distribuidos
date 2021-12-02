using Contracts.Api.Application.Queries.ViewModels;

namespace Contracts.Api.Application.Queries.Mappers
{
	public interface IPreContractLogDetailMapper
	{
		PreContractLogDetailViewModel MapToPreContractLogDetailViewModel(dynamic r);
	}

	public class PreContractLogDetailMapper : IPreContractLogDetailMapper
	{
		public PreContractLogDetailViewModel MapToPreContractLogDetailViewModel(dynamic r)
		{
			PreContractLogDetailViewModel o = new PreContractLogDetailViewModel();

			o.log_detail_id = r.log_detail_id;
			o.document_id = r.document_id;
			o.business_name = r.business_name;
			o.id_summa = r.id_summa;
			o.item = r.item;
			o.segment = r.segment;
			o.commission_variable = r.commission_variable;
			o.category_id = r.category_id;
			o.category_name = r.category_name;
			o.validity = r.validity;
			o.commisison_type = r.commisison_type;
			o.month_range_commission_variable = r.month_range_commission_variable;
			o.percentage_commission_variable = r.percentage_commission_variable;
			o.fixed_commission = r.fixed_commission;
			o.month_range_fixed_commission = r.month_range_fixed_commission;
			o.fixed_commisison_amount = r.fixed_commisison_amount;
			o.start_date_contract = r.start_date_contract;
			o.bank_name = r.bank_name;
			o.bank_account = r.bank_account;
			o.interbank_account = r.interbank_account;
			o.currency_name = r.currency_name;
			o.bank_account_type_name = r.bank_account_type_name;
			o.observation = r.observation;
			o.state = r.state;
			o.log_id = r.log_id;
			o.register_user_id = r.register_user_id;
			o.register_user_fullname = r.register_user_fullname;
			o.register_datetime = r.register_datetime;

			return o;
		}
	}
}
