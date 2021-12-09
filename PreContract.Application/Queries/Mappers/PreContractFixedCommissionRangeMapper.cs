using PreContracts.Api.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreContracts.Api.Application.Queries.Mappers
{
	public interface IPreContractFixedCommissionRangeMapper
	{
		PreContractFixedCommissionRangeViewModel MapToPreContractFixedCommissionRangeViewModel(dynamic r);
	}

	public class PreContractFixedCommissionRangeMapper : IPreContractFixedCommissionRangeMapper
	{
		public PreContractFixedCommissionRangeViewModel MapToPreContractFixedCommissionRangeViewModel(dynamic r)
		{
			PreContractFixedCommissionRangeViewModel o = new PreContractFixedCommissionRangeViewModel();

			o.contract_fixed_com_range_id = r.contract_fixed_com_range_id;
			o.contract_id = r.contract_id;
			o.contract_version = r.contract_version;
			o.contract_modification = r.contract_modification;
			o.validity_time = r.validity_time;
			o.amount = r.amount;
			o.validity_start_date = r.validity_start_date;
			o.validity_end_date = r.validity_end_date;
			o.validity_active = r.validity_active;
			o.state = r.state;
			o.grade = r.grade;
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
