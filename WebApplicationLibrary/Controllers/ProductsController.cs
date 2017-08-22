using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationLibrary.Data.Service;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace WebApplicationLibrary.Controllers
{
    [Produces("application/json")]
    [Route("api/Products")]
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
        public IActionResult GetProduct(int ProductID)
        {
            if (!_productRepository.ProductsExists(ProductID))
            {
                return NotFound();
            }
            var productFromRepo = _productRepository.GetProduct(ProductID);
            return Ok(productFromRepo);
        }
    }
}