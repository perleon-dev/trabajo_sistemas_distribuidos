using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Contracts.Application.Queries.Interfaces;
using Contracts.Application.Queries.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PrePreContracts.Api.Controllers
{
	[Route("tradenames")]
	[ApiController]
	public class TradenameController : ControllerBase
	{
		readonly ITradenameQuery _iTradenameQuery;
		readonly IMediator _mediator;

		public TradenameController(ITradenameQuery iTradenameQuery,IMediator mediator)
		{
			_iTradenameQuery = iTradenameQuery ?? throw new ArgumentNullException(nameof(iTradenameQuery));
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}

		[HttpGet]
		[Route("search")]
		[ProducesResponseType(typeof(IEnumerable<TradenameViewModel>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetBySearch([FromQuery] TradenameRequest request)
		{
			var result = await _iTradenameQuery.GetBySearch(request);

			return Ok(result);
		}

	}
}