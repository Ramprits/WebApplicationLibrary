using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WebApplicationLibrary.Services;
using WebApplicationLibrary.Models;
using WebApplicationLibrary.Entities;

namespace WebApplicationLibrary.Controllers
{
    [Produces("application/json"), Route("api/AuthorCollection")]
    public class AuthorCollectionController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILibraryRepository _repo;
        public AuthorCollectionController(IMapper mapper, ILibraryRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpPost]
        public IActionResult Post([FromBody] IEnumerable<AuthorForCreationDto> authorForCreationDto)
        {
            if (authorForCreationDto == null)
            {
                return BadRequest();
            }
            var authorForCreation = _mapper.Map<IEnumerable<Author>>(authorForCreationDto);
            foreach (var author in authorForCreation)
            {
                _repo.AddAuthor(author);
            }
            if (!_repo.Save())
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}