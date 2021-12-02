using Contracts.Api.Application.Queries.ViewModels;

namespace Contracts.Api.Application.Queries.Mappers
{
	public interface IPreContractLogMapper
	{
		PreContractLogViewModel MapToPreContractLogViewModel(dynamic r);
	}

	public class PreContractLogMapper : IPreContractLogMapper
	{
		public PreContractLogViewModel MapToPreContractLogViewModel(dynamic r)
		{
			PreContractLogViewModel o = new PreContractLogViewModel();

			o.log_id = r.log_id;
			o.file_name = r.file_name;
			o.number_record = r.number_record;
			o.contract_type = r.contract_type;
			o.state = r.state;
			o.register_user_id = r.register_user_id;
			o.register_user_fullname = r.register_user_fullname;
			o.register_datetime = r.register_datetime;

			return o;
		}
	}
}
