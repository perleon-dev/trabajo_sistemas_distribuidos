using PreContracts.Api.Application.Queries.ViewModels;

namespace PreContracts.Api.Application.Queries.Mappers
{
	public interface ILogContractMapper
	{
		LogContractViewModel MapToLogContractViewModel(dynamic r);
	}

	public class LogContractMapper : ILogContractMapper
	{
		public LogContractViewModel MapToLogContractViewModel(dynamic r)
		{
			LogContractViewModel o = new LogContractViewModel();

			o.logContractId =  r.log_contract_id;
			o.typeProcessId =  r.type_process_id;
			o.fileStorageId =  r.file_storage_id;
			o.state =  r.state;
			o.errorMessage = r.error_message;
			o.registerUserId =  r.register_user_id;
			o.registerUserFullname =  r.register_user_fullname;
			o.registerDatetime =  r.register_datetime;
			o.updateUserId =  r.update_user_id;
			o.updateUserFullname =  r.update_user_fullname;
			o.updateDatetime =  r.update_datetime;

			return o;
		}
	}
}