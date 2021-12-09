using PreContracts.Api.Application.Queries.ViewModels;

namespace PreContracts.Api.Application.Queries.Mappers
{
	public interface IPreContractBankAccountMapper
	{
		PreContractBankAccountViewModel MapToPreContractBankAccountViewModel(dynamic r);
	}

	public class PreContractBankAccountMapper : IPreContractBankAccountMapper
	{
		public PreContractBankAccountViewModel MapToPreContractBankAccountViewModel(dynamic r)
		{
			PreContractBankAccountViewModel o = new PreContractBankAccountViewModel();

			o.contract_bank_account_id = r.contract_bank_account_id;
			o.contract_id = r.contract_id;
			o.contract_version = r.contract_version;
			o.contract_modification = r.contract_modification;
			o.bank_id = r.bank_id;
			o.currency_id = r.currency_id;
			o.account_number = r.account_number;
			o.cci_account_number = r.cci_account_number;
			o.type_account = r.type_account;
			o.state = r.state;
			o.register_user_id = r.register_user_id;
			o.register_user_fullname = r.register_user_fullname;
			o.register_datetime = r.register_datetime;
			o.update_user_id = r.update_user_id;
			o.update_user_fullname = r.update_user_fullname;
			o.update_datetime = r.update_datetime;

			return o;
		}
	}
}
