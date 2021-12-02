using System;
using Contracts.Api.Domain.Core;

namespace Contracts.Api.Domain.Aggregates.LogContractAggregate
{
	public class LogContract : Entity
	{
		public int logContractId { get; set; }
		public int typeProcessId { get; }
		public int? fileStorageId { get; }
		public int state { get; }
		public string errorMessage { get; set; }
		public int registerUserId { get; }
		public string registerUserFullname { get; }
		public DateTime registerDatetime { get; }
		public int? updateUserId { get; }
		public string updateUserFullname { get; }
		public DateTime? updateDatetime { get; }

		public LogContract()
		{
		}

		public LogContract( int typeProcessId, int? fileStorageId, int state, string errorMessage, int registerUserId, string registerUserFullname, DateTime registerDatetime, int? updateUserId, string updateUserFullname, DateTime? updateDatetime)
		{
			this.typeProcessId = typeProcessId;
			this.fileStorageId = fileStorageId;
			this.state = state;
			this.errorMessage = errorMessage;
			this.registerUserId = registerUserId;
			this.registerUserFullname = registerUserFullname;
			this.registerDatetime = registerDatetime;
			this.updateUserId = updateUserId;
			this.updateUserFullname = updateUserFullname;
			this.updateDatetime = updateDatetime;
		}

		public LogContract( int logContractId, int typeProcessId, int? fileStorageId, int state, string errorMessage, int registerUserId, string registerUserFullname, DateTime registerDatetime, int? updateUserId, string updateUserFullname, DateTime? updateDatetime)
		{
			this.logContractId = logContractId;
			this.typeProcessId = typeProcessId;
			this.fileStorageId = fileStorageId;
			this.state = state;
			this.errorMessage = errorMessage;
			this.registerUserId = registerUserId;
			this.registerUserFullname = registerUserFullname;
			this.registerDatetime = registerDatetime;
			this.updateUserId = updateUserId;
			this.updateUserFullname = updateUserFullname;
			this.updateDatetime = updateDatetime;
		}
	}
}