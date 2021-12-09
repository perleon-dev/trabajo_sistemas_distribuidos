using Contracts.Aplication.Queries.Interfaces;
using Contracts.Aplication.Queries.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PrePreContracts.Api.Controllers
{
	[Route("sellers/idsumma")]
	[ApiController]
	public class IdSummaController : ControllerBase
	{
		readonly IIdSummaQuery _iIdSummaQuery;
		readonly IMediator _mediator;

		public IdSummaController(IIdSummaQuery iIdSummaQuery, IMediator mediator)
		{
			_iIdSummaQuery = iIdSummaQuery ?? throw new ArgumentNullException(nameof(iIdSummaQuery));
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}

		[HttpGet]
		[Route("search")]
		[ProducesResponseType(typeof(IEnumerable<IdSummaViewModel>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetBySearch([FromQuery] IdSummaRequest request)
		{
			var result = await _iIdSummaQuery.GetBySearch(request);

			return Ok(result);
		}
	}
}
