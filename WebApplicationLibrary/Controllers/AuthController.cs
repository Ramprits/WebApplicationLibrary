using Microsoft.AspNetCore.Mvc;
using WebApplicationLibrary.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using AutoMapper;
using WebApplicationLibrary.Models;
using System.Threading.Tasks;

namespace WebApplicationLibrary.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController : Controller
    {
        private readonly LibraryContext _context;
        private readonly SignInManager<CampUser> _signInMgr;
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;

        public AuthController(LibraryContext context, SignInManager<CampUser> signMgr,
            ILogger<AuthController> logger, IMapper mapper)
        {
            _context = context;
            _signInMgr = signMgr;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] CreadentialModel model)
        {
            try
            {
                var result = await _signInMgr.PasswordSignInAsync(model.UserName, model.Password, false, false);
                if (result.Succeeded)
                {
                    return Ok();
                }
            }
            catch (System.Exception ex)
            {

                _logger.LogError($"Exception throw while loggin {ex}");
            }
            return BadRequest("Failed login");
        }
    }
}