using Contracts.Aplication.Queries.Interfaces;
using Contracts.Aplication.Queries.Querys;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace PreContracts.Api.Controllers
{
    [Route("customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerQueryHandler _customerQueryHandler;
        readonly IMediator _mediator;

        public CustomerController(ICustomerQueryHandler customerStandardQueryHandler, IMediator mediator)
        {
            _mediator = mediator;
            _customerQueryHandler = customerStandardQueryHandler;
        }


        [HttpGet()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByFiltersAsync([FromQuery] CustomerQuery query)
        {
            var result = await _customerQueryHandler.GetByFiltersAsync(query);

            return Ok(result);
        }
    }
}
