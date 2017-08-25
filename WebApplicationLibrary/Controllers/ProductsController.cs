using Microsoft.AspNetCore.Mvc;
using WebApplicationLibrary.Data.Service;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace WebApplicationLibrary.Controllers
{
    [Produces("application/json"), Route("api/Products")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, ILogger<ProductsController> logger, IMapper mapper)
        {
            _productRepository = productRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetProducts")]
        public IActionResult GetProducts()
        {
            var products = _productRepository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{ProductID}")]
        public IActionResult GetProduct(int productId)
        {
            if (!_productRepository.ProductsExists(productId))
            {
                return NotFound();
            }
            var productFromRepo = _productRepository.GetProduct(productId);
            return Ok(productFromRepo);
        }
    }
}