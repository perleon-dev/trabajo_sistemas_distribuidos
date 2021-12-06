using Contracts.Api.Application.Queries.ViewModels;
using System;

namespace Contracts.Aplication.Queries.ViewModel
{
	public class CustomerSummaViewModel
	{
		public int customer_summa_id { get; set; }
		public string document_id { get; set; }
		public int? id_summa { get; set; }
		public int? register_user_id { get; set; }
		public string register_user_fullname { get; set; }
		public DateTime? register_datetime { get; set; }
		public int? update_user_id { get; set; }
		public string update_user_fullname { get; set; }
		public DateTime? update_datetime { get; set; }
		public string tradename { get; set; }
	}


	public class CustomerSummaRequest : Pagination
	{
		public int customer_summa_id { get; set; }
		public string documentId { get; set; }
		public string listDocumentId { get; set; }
		public string listIdSumma { get; set; }
	}
}
