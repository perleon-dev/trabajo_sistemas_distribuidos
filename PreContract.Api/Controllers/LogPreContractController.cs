using Contracts.Api.Application.Queries.Interfaces;
using Contracts.Api.Application.Queries.ViewModels;
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
    [Route("contracts/precontracts/logs")]
    [ApiController]
    public class LogPreContractController : ControllerBase
    {
        readonly IPreContractLogQuery IPreContractLogQuery_;

        public LogPreContractController(IPreContractLogQuery iPreContractLogQuery) 
        {
            IPreContractLogQuery_ = iPreContractLogQuery;
        }

        [HttpGet]
        [Route("search")]
        [ProducesResponseType(typeof(IEnumerable<PreContractLogViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBySearch([FromQuery] PreContractLogRequest request)
        {
            var result = await IPreContractLogQuery_.GetBySearch(request);

            return Ok(result);
        }
    }
}
