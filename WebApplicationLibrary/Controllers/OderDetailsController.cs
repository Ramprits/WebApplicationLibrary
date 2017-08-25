using Microsoft.AspNetCore.Mvc;
using WebApplicationLibrary.Data.Service;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace WebApplicationLibrary.Controllers
{
    [Produces("application/json"), Route("api/Products/{ProductID}/OderDetails")]
    public class OderDetailsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public OderDetailsController(IProductRepository productRepository, ILogger<OderDetailsController> logger, IMapper mapper)
        {
            _productRepository = productRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetOderDetails(int productId)
        {
            if (!_productRepository.ProductsExists(productId))
                return NotFound();
            var oderDetailFromRepo = _productRepository.GetOrderDetailsForProducts(productId);
            return Ok(oderDetailFromRepo);
        }
    }
}