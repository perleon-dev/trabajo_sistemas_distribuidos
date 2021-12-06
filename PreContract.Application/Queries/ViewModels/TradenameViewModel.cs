
using Contracts.Api.Application.Queries.ViewModels;
using System;

namespace Contracts.Application.Queries.ViewModels
{
	public class TradenameViewModel
	{
		public int TradenameId { get; set; }
		public string TradenameName { get; set; }
	}

	public class TradenameRequest : PaginationRequest
	{
		public int? TradenameId { get; set; }
		public string tradeName { get; set; }
        public string CustomerId { get; set; }
    }
}