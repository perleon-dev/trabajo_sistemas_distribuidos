using Contracts.Api.Application.Commands.PreContractVariableCommissionRangeCommand;
using Contracts.Api.Application.Queries.Interfaces;
using Contracts.Api.Application.Queries.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PreContracts.Api.Controllers
{
	[Authorize]
	[Route("contracts/pre-contract/variable-commission-ranges")]
	[ApiController]
	public class PreContractVariableCommissionRangeController : ControllerBase
	{
		readonly IPreContractVariableCommissionRangeQuery _iPreContractVariableCommissionRangeQuery;
		readonly IMediator _mediator;

		public PreContractVariableCommissionRangeController(IPreContractVariableCommissionRangeQuery iPreContractVariableCommissionRangeQuery, IMediator mediator)
		{
			_iPreContractVariableCommissionRangeQuery = iPreContractVariableCommissionRangeQuery ?? throw new ArgumentNullException(nameof(iPreContractVariableCommissionRangeQuery));
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}

		[HttpGet]
		[Route("{contract_variable_com_range_id}")]
		[ProducesResponseType(typeof(PreContractVariableCommissionRangeViewModel), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> GetById(int contract_variable_com_range_id)
		{
			var result = await _iPreContractVariableCommissionRangeQuery.GetById(contract_variable_com_range_id);

			if (result != null)
				return Ok(result);
			else
				return NotFound();
		}

		[HttpGet]
		[Route("search")]
		[ProducesResponseType(typeof(IEnumerable<PreContractVariableCommissionRangeViewModel>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetBySearch([FromQuery] PreContractVariableCommissionRangeRequest request)
		{
			var result = await _iPreContractVariableCommissionRangeQuery.GetBySearch(request);

			return Ok(result);
		}

		[HttpGet]
		[Route("find-all")]
		[ProducesResponseType(typeof((IEnumerable<PreContractVariableCommissionRangeViewModel>, int)), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetByFindAll([FromQuery] PreContractVariableCommissionRangeRequest request)
		{
			var result = await _iPreContractVariableCommissionRangeQuery.GetByFindAll(request);

			return Ok(result);
		}

		[HttpPost]
		[ProducesResponseType((int)HttpStatusCode.Created)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> CreatePreContractVariableCommissionRange(CreatePreContractVariableCommissionRangeCommand command)
		{
			var result = await _mediator.Send(command);

			return CreatedAtAction(nameof(CreatePreContractVariableCommissionRange), result);
		}

		[HttpPut]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> UpdatePreContractVariableCommissionRange(UpdatePreContractVariableCommissionRangeCommand command)
		{
			var result = await _mediator.Send(command);

			return Ok(result);
		}
	}
}
