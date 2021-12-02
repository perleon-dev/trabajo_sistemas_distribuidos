using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Contracts.Api.Application.Queries.Interfaces;
using Contracts.Api.Application.Queries.ViewModels;
using Contracts.Api.Application.Commands.LogContractCommand;

namespace PreContracts.API.Controllers
{
	[Authorize]
	[Route("contracts/logs")]
	[ApiController]
	public class LogContractController : ControllerBase
	{
		readonly ILogContractQuery _iLogContractQuery;
		readonly IMediator _mediator;

		public LogContractController(ILogContractQuery iLogContractQuery,IMediator mediator)
		{
			_iLogContractQuery = iLogContractQuery ?? throw new ArgumentNullException(nameof(iLogContractQuery));
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}

		[HttpGet]
		[Route("{logContractId}")]
		[ProducesResponseType(typeof(LogContractViewModel), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> GetById(int logContractId)
		{
			var result = await _iLogContractQuery.GetById(logContractId);

			if (result != null)
				return Ok(result);
			else
				return NotFound();
		}

		[HttpGet]
		[Route("search")]
		[ProducesResponseType(typeof(IEnumerable<LogContractViewModel>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetBySearch([FromQuery] LogContractRequest request)
		{
			var result = await _iLogContractQuery.GetBySearch(request);

			return Ok(result);
		}

		[HttpGet]
		[Route("find-all")]
		[ProducesResponseType(typeof(IEnumerable<LogContractViewModel>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetByFindAll([FromQuery] LogContractRequest request)
		{
			var result = await _iLogContractQuery.GetByFindAll(request);

			return Ok(result);
		}

		[HttpPost]
		[ProducesResponseType((int)HttpStatusCode.Created)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> CreateLogContract(CreateLogContractCommand command)
		{
			var result = await _mediator.Send(command);

			return CreatedAtAction(nameof(CreateLogContract), result);
		}

		[HttpPut]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> UpdateLogContract(UpdateLogContractCommand command)
		{
			var result = await _mediator.Send(command);

			return Ok(result);
		}
	}
}