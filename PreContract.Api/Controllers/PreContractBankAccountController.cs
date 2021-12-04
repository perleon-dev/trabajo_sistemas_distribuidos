using Contracts.Api.Application.Commands.PreContractBankAccountCommands;
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
	[Route("contracts/pre-contract/bank-accounts")]
	[ApiController]
	public class PreContractBankAccountController : ControllerBase
	{
		readonly IPreContractBankAccountQuery _iPreContractBankAccountQuery;
		readonly IMediator _mediator;

		public PreContractBankAccountController(IPreContractBankAccountQuery iPreContractBankAccountQuery, IMediator mediator)
		{
			_iPreContractBankAccountQuery = iPreContractBankAccountQuery ?? throw new ArgumentNullException(nameof(iPreContractBankAccountQuery));
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}

		[HttpGet]
		[Route("{contract_bank_account_id}")]
		[ProducesResponseType(typeof(PreContractBankAccountViewModel), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> GetById(int contract_bank_account_id)
		{
			var result = await _iPreContractBankAccountQuery.GetById(contract_bank_account_id);

			if (result != null)
				return Ok(result);
			else
				return NotFound();
		}

		[HttpGet]
		[Route("search")]
		[ProducesResponseType(typeof(IEnumerable<PreContractBankAccountViewModel>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetBySearch([FromQuery] PreContractBankAccountRequest request)
		{
			var result = await _iPreContractBankAccountQuery.GetBySearch(request);

			return Ok(result);
		}

		[HttpGet]
		[Route("find-all")]
		[ProducesResponseType(typeof((IEnumerable<PreContractBankAccountViewModel>, int)), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetByFindAll([FromQuery] PreContractBankAccountRequest request)
		{
			var result = await _iPreContractBankAccountQuery.GetByFindAll(request);

			return Ok(result);
		}

		[HttpPost]
		[ProducesResponseType((int)HttpStatusCode.Created)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> CreatePreContractBankAccount(CreatePreContractBankAccountCommand command)
		{
			var result = await _mediator.Send(command);

			return CreatedAtAction(nameof(CreatePreContractBankAccount), result);
		}

		[HttpPut]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> UpdatePreContractBankAccount(UpdatePreContractBankAccountCommand command)
		{
			var result = await _mediator.Send(command);

			return Ok(result);
		}
	}
}
