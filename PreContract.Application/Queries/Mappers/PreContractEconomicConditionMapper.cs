using PreContracts.Api.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreContracts.Api.Application.Queries.Mappers
{
	public interface IPreContractEconomicConditionMapper
	{
		PreContractEconomicConditionViewModel MapToPreContractEconomicConditionViewModel(dynamic r);
	}

	public class PreContractEconomicConditionMapper : IPreContractEconomicConditionMapper
	{
		public PreContractEconomicConditionViewModel MapToPreContractEconomicConditionViewModel(dynamic r)
		{
			PreContractEconomicConditionViewModel o = new PreContractEconomicConditionViewModel();

			o.economic_condition_id = r.economic_condition_id;
			o.commission = r.commission;
			o.category_id = r.category_id;
			o.state = r.state;
			o.contract_id = r.contract_id;
			o.contract_version = r.contract_version;
			o.contract_modification = r.contract_modification;
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
