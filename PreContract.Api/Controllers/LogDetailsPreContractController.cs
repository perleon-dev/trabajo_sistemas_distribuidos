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
    [Route("contracts/precontracts/logsDetail")]
    [ApiController]
    public class LogDetailsPreContractController : ControllerBase
    {
        readonly IPreContractLogDetailQuery IPreContractLogDetailQuery_;

        public LogDetailsPreContractController(IPreContractLogDetailQuery iPreContractLogDetailQuery)
        {
            IPreContractLogDetailQuery_ = iPreContractLogDetailQuery;
        }

        [HttpGet]
        [Route("search")]
        [ProducesResponseType(typeof(IEnumerable<PreContractLogDetailViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBySearch([FromQuery] PreContractLogDetailRequest request)
        {
            var result = await IPreContractLogDetailQuery_.GetBySearch(request);

            return Ok(result);
        }
    }
}
