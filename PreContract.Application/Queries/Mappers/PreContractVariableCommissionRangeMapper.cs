using Contracts.Api.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Api.Application.Queries.Mappers
{
	public interface IPreContractVariableCommissionRangeMapper
	{
		PreContractVariableCommissionRangeViewModel MapToPreContractVariableCommissionRangeViewModel(dynamic r);
	}

	public class PreContractVariableCommissionRangeMapper : IPreContractVariableCommissionRangeMapper
	{
		public PreContractVariableCommissionRangeViewModel MapToPreContractVariableCommissionRangeViewModel(dynamic r)
		{
			PreContractVariableCommissionRangeViewModel o = new PreContractVariableCommissionRangeViewModel();

			o.contract_variable_com_range_id = r.contract_variable_com_range_id;
			o.contract_id = r.contract_id;
			o.contract_version = r.contract_version;
			o.contract_modification = r.contract_modification;
			o.contract_tradename_id = r.contract_tradename_id;
			o.state = r.state;
			o.validity_time = r.validity_time;
			o.validity_active = r.validity_active;
			o.percentage = r.percentage;
			o.validity_start_date = r.validity_start_date;
			o.validity_end_date = r.validity_end_date;
			o.grade = r.grade;
			o.register_user_id = r.register_user_id;
			o.register_user_fullname = r.register_user_fullname;
			o.register_datetime = r.register_datetime;
			o.update_user_id = r.update_user_id;
			o.update_user_fullname = r.update_user_fullname;
			o.update_datetime = r.update_datetime;
			o.category_id = r.category_id;

			return o;
		}
	}
}
