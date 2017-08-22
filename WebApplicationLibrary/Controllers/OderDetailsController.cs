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
    [Route("api/Products/{ProductID}/OderDetails")]
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
        public IActionResult GetOderDetails(int ProductId)
        {
            if (!_productRepository.ProductsExists(ProductId))
            {
                return NotFound();
            }
            var OderDetailFromRepo = _productRepository.GetOrderDetailsForProducts(ProductId);
            return Ok(OderDetailFromRepo);
        }
    }
}