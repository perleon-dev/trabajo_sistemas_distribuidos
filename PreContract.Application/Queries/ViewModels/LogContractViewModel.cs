
using System;

namespace Contracts.Api.Application.Queries.ViewModels
{
	public class LogContractViewModel
	{
		public int logContractId { get; set; }
		public int typeProcessId { get; set; }
		public int? fileStorageId { get; set; }
		public int state { get; set; }
		public string errorMessage { get; set; }
		public int registerUserId { get; set; }
		public string registerUserFullname { get; set; }
		public DateTime registerDatetime { get; set; }
		public int? updateUserId { get; set; }
		public string updateUserFullname { get; set; }
		public DateTime? updateDatetime { get; set; }
	}

	public class LogContractRequest : PaginationRequest
	{
		public int logContractId { get; set; }
	}
}