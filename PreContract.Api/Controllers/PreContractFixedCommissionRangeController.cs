using Contracts.Api.Application.Commands.PreContractFixedCommissionRangeCommands;
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
	[Route("contracts/pre-contract/fixed-commission-ranges")]
	[ApiController]
	public class PreContractFixedCommissionRangeController : ControllerBase
	{
		readonly IPreContractFixedCommissionRangeQuery _iPreContractFixedCommissionRangeQuery;
		readonly IMediator _mediator;

		public PreContractFixedCommissionRangeController(IPreContractFixedCommissionRangeQuery iPreContractFixedCommissionRangeQuery, IMediator mediator)
		{
			_iPreContractFixedCommissionRangeQuery = iPreContractFixedCommissionRangeQuery ?? throw new ArgumentNullException(nameof(iPreContractFixedCommissionRangeQuery));
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}

		[HttpGet]
		[Route("{contract_fixed_com_range_id}")]
		[ProducesResponseType(typeof(PreContractFixedCommissionRangeViewModel), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> GetById(int contract_fixed_com_range_id)
		{
			var result = await _iPreContractFixedCommissionRangeQuery.GetById(contract_fixed_com_range_id);

			if (result != null)
				return Ok(result);
			else
				return NotFound();
		}

		[HttpGet]
		[Route("search")]
		[ProducesResponseType(typeof(IEnumerable<PreContractFixedCommissionRangeViewModel>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetBySearch([FromQuery] PreContractFixedCommissionRangeRequest request)
		{
			var result = await _iPreContractFixedCommissionRangeQuery.GetBySearch(request);

			return Ok(result);
		}

		[HttpGet]
		[Route("find-all")]
		[ProducesResponseType(typeof((IEnumerable<PreContractFixedCommissionRangeViewModel>, int)), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetByFindAll([FromQuery] PreContractFixedCommissionRangeRequest request)
		{
			var result = await _iPreContractFixedCommissionRangeQuery.GetByFindAll(request);

			return Ok(result);
		}

		[HttpPost]
		[ProducesResponseType((int)HttpStatusCode.Created)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> CreatePreContractFixedCommissionRange(CreatePreContractFixedCommissionRangeCommand command)
		{
			var result = await _mediator.Send(command);

			return CreatedAtAction(nameof(CreatePreContractFixedCommissionRange), result);
		}

		[HttpPut]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> UpdatePreContractFixedCommissionRange(UpdatePreContractFixedCommissionRangeCommand command)
		{
			var result = await _mediator.Send(command);

			return Ok(result);
		}
	}
}
