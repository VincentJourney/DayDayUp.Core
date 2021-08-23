using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListingController : ControllerBase
    {
        private readonly EngineFactory _engineFactory;

        public ListingController(EngineFactory engineFactory)
        {
            _engineFactory = engineFactory;
        }

        [HttpGet("GetAe")]
        public IActionResult GetAe()
        {
            _engineFactory.Create<AeEngine>().Excute("Ae");
            return Ok();
        }

        [HttpGet("GetEbay")]
        public IActionResult GetEbay()
        {
            _engineFactory.Create<EbayEngine>().Excute("Ebay");
            return Ok();
        }
    }
}
