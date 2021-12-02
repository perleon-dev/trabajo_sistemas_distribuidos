using Contracts.Api.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Api.Application.Queries.Mappers
{
	public interface IPreContractTradenameMapper
	{
		PreContractTradenameViewModel MapToPreContractTradenameViewModel(dynamic r);
	}

	public class PreContractTradenameMapper : IPreContractTradenameMapper
	{
		public PreContractTradenameViewModel MapToPreContractTradenameViewModel(dynamic r)
		{
			PreContractTradenameViewModel o = new PreContractTradenameViewModel();

			o.contract_tradename_id = r.contract_tradename_id;
			o.contract_id = r.contract_id;
			o.contract_version = r.contract_version;
			o.contract_modification = r.contract_modification;
			o.tradename_id = r.tradename_id;
			o.id_summa = r.id_summa;
			o.rubric_id = r.rubric_id;
			o.register_user_id = r.register_user_id;
			o.register_user_fullname = r.register_user_fullname;
			o.register_datetime = r.register_datetime;
			o.update_user_id = r.update_user_id;
			o.update_user_fullname = r.update_user_fullname;
			o.update_datetime = r.update_datetime;
			o.state = r.state;
			o.tradename = r.tradename;

			return o;
		}
	}
}
