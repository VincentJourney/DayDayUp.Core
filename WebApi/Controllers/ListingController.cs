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
        private readonly IListingExcutor _aeListingExcutor;
        private readonly IListingExcutor _ebayListingExcutor;

        public ListingController(EngineFactory engineFactory)
        {
            _aeListingExcutor = engineFactory.Create<AeEngine>();
            _ebayListingExcutor = engineFactory.Create<EbayEngine>();
        }

        [HttpGet("GetAe")]
        public IActionResult GetAe()
        {
            _aeListingExcutor.Excute("ae");
            var aeengine = _aeListingExcutor as AeEngine;
            Console.WriteLine(aeengine.GetHashCode());
            foreach (var item in aeengine._listingExcutors)
            {
                Console.WriteLine(item.GetHashCode());
            }
            return Ok();
        }

        [HttpGet("GetEbay")]
        public IActionResult GetEbay()
        {
            _ebayListingExcutor.Excute("ebay");
            var ebayengine = _ebayListingExcutor as EbayEngine;
            Console.WriteLine(ebayengine.GetHashCode());
            foreach (var item in ebayengine._listingExcutors)
            {
                Console.WriteLine(item.GetHashCode());
            }
            return Ok();
        }
    }
}
