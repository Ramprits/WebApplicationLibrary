using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WebApplicationLibrary.Services;
using WebApplicationLibrary.Models;
using Microsoft.AspNetCore.Cors;
using WebApplicationLibrary.Entities;
using WebApplicationLibrary.Filter;

namespace WebApplicationLibrary.Controllers
{
    [Produces("application/json")]
    [Route("api/Authors")]
    [EnableCors("AnyGET")]
    [ValidateModel]
    public class AuthorsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILibraryRepository _repo;
        public AuthorsController(IMapper mapper, ILibraryRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet()]
        public IActionResult GetAuthors()
        {
            return base.Ok(_mapper.Map<IEnumerable<AuthorDto>>(_repo.GetAuthors()));

        }

        [HttpGet("{authorId}", Name = "GetAuthors")]
        public IActionResult GetAuthors(Guid authorId)
        {
            var authoreFromRepo = _repo.GetAuthor(authorId);
            if (authoreFromRepo == null)
                return NotFound($"Author with {authoreFromRepo} not found !");
            return Ok(authoreFromRepo);

        }

        [HttpPost]
        public IActionResult Post([FromBody] AuthorForCreationDto author)
        {
            if (author == null) return BadRequest();
            var authorEntity = _mapper.Map<Author>(author);
            _repo.AddAuthor(authorEntity);
            if (!_repo.Save())
            {
                throw new Exception($" Creating an author is failed !");
            }
            var authorToReturn = _mapper.Map<AuthorDto>(authorEntity);

            return Created("GetAuthors", new { authorId = authorToReturn.Id });
        }

        [HttpDelete("{authorId}")]
        public IActionResult Delete(Guid authorId)
        {
            if (!_repo.AuthorExists(authorId))
            {
                return NotFound($"Author with {authorId} is not found !");
            }
            var getAuthorFromRepo = _repo.GetAuthor(authorId);
            _repo.DeleteAuthor(getAuthorFromRepo);
            if (!_repo.Save())
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpPut("{authorId}")]
        public IActionResult Put(Guid authorId, [FromBody] AuthorForUpdateDto author)
        {
            if (author == null)
            {
                return NotFound();
            }
            if (!_repo.AuthorExists(authorId))
            {
                return BadRequest();
            }
            var authorFromRepo = _repo.GetAuthor(authorId);
            _mapper.Map(author, authorFromRepo);
            if (!_repo.Save())
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}