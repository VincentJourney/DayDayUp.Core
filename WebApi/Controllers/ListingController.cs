using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("Ae")]
        public IActionResult Ae()
        {
            _engineFactory.Create<AeEngine>().Excute("Ae");
            return Ok();
        }

        [HttpGet("Ebay")]
        public IActionResult Ebay()
        {
            _engineFactory.Create<EbayEngine>().Excute("Ebay");
            return Ok();
        }
    }
}
