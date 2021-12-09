using Contracts.Aplication.Queries.Interfaces;
using Contracts.Aplication.Queries.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace PrePreContracts.Api.Controllers
{
	[Route("sellers/customersumma")]
	[ApiController]
	public class CustomerSummaController : ControllerBase
	{
		readonly ICustomerSummaQuery _iCustomerSummaQuery;
		readonly IMediator _mediator;

		public CustomerSummaController(ICustomerSummaQuery iCustomerSummaQuery, IMediator mediator)
		{
			_iCustomerSummaQuery = iCustomerSummaQuery ?? throw new ArgumentNullException(nameof(iCustomerSummaQuery));
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}

		[HttpGet]
		[Route("search")]
		[ProducesResponseType(typeof(IEnumerable<CustomerSummaViewModel>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetBySearch([FromQuery] CustomerSummaRequest request)
		{
			var result = await _iCustomerSummaQuery.GetBySearch(request);

			return Ok(result);
		}
	}
}
