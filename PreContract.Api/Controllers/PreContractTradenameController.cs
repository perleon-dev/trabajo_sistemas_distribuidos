using PreContracts.Api.Application.Commands.PreContractTradenameCommands;
using PreContracts.Api.Application.Queries.Interfaces;
using PreContracts.Api.Application.Queries.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PrePreContracts.Api.Controllers
{
	[Route("contracts/pre-contract/tradenames")]
	[ApiController]
	public class PreContractTradenameController : ControllerBase
	{
		readonly IPreContractTradenameQuery _iPreContractTradenameQuery;
		readonly IMediator _mediator;

		public PreContractTradenameController(IPreContractTradenameQuery iPreContractTradenameQuery, IMediator mediator)
		{
			_iPreContractTradenameQuery = iPreContractTradenameQuery ?? throw new ArgumentNullException(nameof(iPreContractTradenameQuery));
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}

		[HttpGet]
		[Route("{contract_tradename_id}")]
		[ProducesResponseType(typeof(PreContractTradenameViewModel), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> GetById(int contract_tradename_id)
		{
			var result = await _iPreContractTradenameQuery.GetById(contract_tradename_id);

			if (result != null)
				return Ok(result);
			else
				return NotFound();
		}

		[HttpGet]
		[Route("search")]
		[ProducesResponseType(typeof(IEnumerable<PreContractTradenameViewModel>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetBySearch([FromQuery] PreContractTradenameRequest request)
		{
			var result = await _iPreContractTradenameQuery.GetBySearch(request);

			return Ok(result);
		}

		[HttpGet]
		[Route("find-all")]
		[ProducesResponseType(typeof((IEnumerable<PreContractTradenameViewModel>, int)), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetByFindAll([FromQuery] PreContractTradenameRequest request)
		{
			var result = await _iPreContractTradenameQuery.GetByFindAll(request);

			return Ok(result);
		}

		[HttpPost]
		[ProducesResponseType((int)HttpStatusCode.Created)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> CreatePreContractTradename(CreatePreContractTradenameCommand command)
		{
			var result = await _mediator.Send(command);

			return CreatedAtAction(nameof(CreatePreContractTradename), result);
		}

		[HttpPut]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> UpdatePreContractTradename(UpdatePreContractTradenameCommand command)
		{
			var result = await _mediator.Send(command);

			return Ok(result);
		}
	}
}
