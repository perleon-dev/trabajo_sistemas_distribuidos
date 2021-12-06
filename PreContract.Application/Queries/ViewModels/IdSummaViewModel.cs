using Contracts.Api.Application.Queries.ViewModels;
using System;

namespace Contracts.Aplication.Queries.ViewModel
{
	public class IdSummaViewModel
	{
		public int id_summa { get; set; }
		public string tradename { get; set; }
		public int? register_user_id { get; set; }
		public string register_user_fullname { get; set; }
		public DateTime? register_datetime { get; set; }
		public int? update_user_id { get; set; }
		public string update_user_fullname { get; set; }
		public DateTime? update_datetime { get; set; }
	}

	public class IdSummaRequest : Pagination
	{
		public int? id_summa { get; set; }
        public string tradename { get; set; }
		public string tradename_list { get; set; }
	}

}
