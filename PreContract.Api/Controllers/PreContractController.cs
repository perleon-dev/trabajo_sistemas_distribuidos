using Contracts.Api.Application.Commands.PreContractCommands;
using Contracts.Api.Application.Queries.Interfaces;
using Contracts.Api.Application.Queries.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace PreContracts.Api.Controllers
{
	[Route("contracts/pre-contract")]
	[ApiController]
	public class PreContractController : ControllerBase
	{
		readonly IPreContractQuery _iPreContractQuery;
		readonly IMediator _mediator;

		public PreContractController(IPreContractQuery iPreContractQuery, IMediator mediator)
		{
			_iPreContractQuery = iPreContractQuery ?? throw new ArgumentNullException(nameof(iPreContractQuery));
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}

		[HttpGet]
		[Route("{contract_id}")]
		[ProducesResponseType(typeof(PreContractViewModel), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> GetById(int contract_id)
		{
			var result = await _iPreContractQuery.GetById(contract_id);

			if (result != null)
				return Ok(result);
			else
				return NotFound();
		}

		[HttpGet]
		[Route("search")]
		[ProducesResponseType(typeof(IEnumerable<PreContractViewModel>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetBySearch([FromQuery] PreContractRequest request)
		{
			var result = await _iPreContractQuery.GetBySearch(request);

			return Ok(result);
		}

		[HttpGet]
		[Route("find-all")]
		[ProducesResponseType(typeof((IEnumerable<PreContractViewModel>, int)), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetByFindAll([FromQuery] PreContractRequest request)
		{
			var result = await _iPreContractQuery.GetByFindAll(request);

			return Ok(result);
		}

		[HttpPost]
		[ProducesResponseType((int)HttpStatusCode.Created)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> CreatePreContract(CreatePreContractCommand command)
		{
			var result = await _mediator.Send(command);

			return CreatedAtAction(nameof(CreatePreContract), result);
		}

		[HttpPut]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> UpdatePreContract(UpdatePreContractCommand command)
		{
			var result = await _mediator.Send(command);

			return Ok(result);
		}

		[HttpGet("download-template-seller-center")]
		[ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> DonwloadTemplateContractMarketPlace()
		{
			var result = await _iPreContractQuery.DownloadTemplateContractMarketPlace();
			return Ok(new { file = result, fileName = $"FORMATO_CONTRATO_SELLER_CENTER_{DateTime.Now.ToString("ddMMyyyyhhmmss")}.xlsx" });
		}

		[HttpGet("download-template-vtex")]
		[ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> DonwloadTemplateContractMarketPlaceVTex()
		{
			var result = await _iPreContractQuery.DownloadTemplateContractMarketPlaceVTex();
			return Ok(new { file = result, fileName = $"FORMATO_CONTRATO_VTEX_{DateTime.Now.ToString("ddMMyyyyhhmmss")}.xlsx" });
		}

		[HttpGet("download-template-vtex-to-vtex")]
		[ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> DonwloadTemplateContractMarketPlaceVTexToVTex()
		{
			var result = await _iPreContractQuery.DownloadTemplateContractMarketPlaceVTexToVTex();
			return Ok(new { file = result, fileName = $"FORMATO_CONTRATO_VTEX_TO_VTEX_{DateTime.Now.ToString("ddMMyyyyhhmmss")}.xlsx" });
		}

		[HttpPost("create-masive")]
		[ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> CreatePreContractMasisve([FromForm] CreatePreContractMasisveCommand createPreContractMasisveCommand)
		{
			var contracts = await _mediator.Send(createPreContractMasisveCommand);
			return Ok(contracts);
		}

		[HttpPost("send-precontract")]
		[ProducesResponseType((int)HttpStatusCode.Created)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> SendPreContract(SendPreContractCommand command)
		{
			var result = await _mediator.Send(command);

			return CreatedAtAction(nameof(SendPreContract), result);
		}

		[HttpPut("update-massive-state")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> UpdateMassiveStatePreContract(UpdateMassiveStatePreContractCommand command)
		{
			var result = await _mediator.Send(command);

			return Ok(result);
		}
	}
}
