using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WebApplicationLibrary.Services;
using WebApplicationLibrary.Models;
using Microsoft.AspNetCore.Cors;
using WebApplicationLibrary.Entities;
using WebApplicationLibrary.Filter;
using WebApplicationLibrary.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace WebApplicationLibrary.Controllers
{
    [Authorize, Produces("application/json"), Route("api/Authors"), EnableCors("AnyGET"), ValidateModel]
    public class AuthorsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILibraryRepository _libraryRepository;
        private IUrlHelper _urlHelper;
        private readonly UserManager<CampUser> _userManager;

        public AuthorsController(IMapper mapper, ILibraryRepository repo, IUrlHelper urlHelper, UserManager<CampUser> userManager
           )
        {
            _mapper = mapper;
            _libraryRepository = repo;
            _urlHelper = urlHelper;
            _userManager = userManager;
        }

        [HttpGet(Name = "GetAuthors")]
        public IActionResult GetAuthors(AuthorsResourceParameters authorsResourceParameters)
        {
            var authorsFromRepo = _libraryRepository.GetAuthors(authorsResourceParameters);
            var author = _mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo);
            return base.Ok(author);

        }

        private string CreateAuthorsResourceUri(
              AuthorsResourceParameters authorsResourceParameters,
              ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return _urlHelper.Link("GetAuthors",
                      new
                      {
                          fields = authorsResourceParameters.Fields,
                          orderBy = authorsResourceParameters.OrderBy,
                          searchQuery = authorsResourceParameters.SearchQuery,
                          genre = authorsResourceParameters.Genre,
                          pageNumber = authorsResourceParameters.PageNumber - 1,
                          pageSize = authorsResourceParameters.PageSize
                      });
                case ResourceUriType.NextPage:
                    return _urlHelper.Link("GetAuthors",
                      new
                      {
                          fields = authorsResourceParameters.Fields,
                          orderBy = authorsResourceParameters.OrderBy,
                          searchQuery = authorsResourceParameters.SearchQuery,
                          genre = authorsResourceParameters.Genre,
                          pageNumber = authorsResourceParameters.PageNumber + 1,
                          pageSize = authorsResourceParameters.PageSize
                      });

                default:
                    return _urlHelper.Link("GetAuthors",
                    new
                    {
                        fields = authorsResourceParameters.Fields,
                        orderBy = authorsResourceParameters.OrderBy,
                        searchQuery = authorsResourceParameters.SearchQuery,
                        genre = authorsResourceParameters.Genre,
                        pageNumber = authorsResourceParameters.PageNumber,
                        pageSize = authorsResourceParameters.PageSize
                    });
            }
        }


        [HttpGet("{authorId}", Name = "GetAuthor")]
        public IActionResult GetAuthor(Guid authorId)
        {
            var campUser = _userManager.FindByNameAsync(this.User.Identity.Name);
            if (campUser != null)
            {
                var authoreFromRepo = _libraryRepository.GetAuthor(authorId);
                if (authoreFromRepo == null)
                    return NotFound($"Author with {authoreFromRepo} not found !");
                return Ok(authoreFromRepo);
            }
            return BadRequest();


        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AuthorForCreationDto author)
        {
            if (author == null) return BadRequest();
            var authorEntity = _mapper.Map<Author>(author);
            var campUser = await _userManager.FindByNameAsync(this.User.Identity.Name);
            if (campUser != null)
            {
                authorEntity.User = campUser;
                _libraryRepository.AddAuthor(authorEntity);
            }

            if (!_libraryRepository.Save())
            {
                throw new Exception($" Creating an author is failed !");
            }
            var authorToReturn = _mapper.Map<AuthorDto>(authorEntity);

            return Created("GetAuthors", new { authorId = authorToReturn.Id });
        }

        [HttpDelete("{authorId}")]
        public IActionResult Delete(Guid authorId)
        {
            if (!_libraryRepository.AuthorExists(authorId))
            {
                return NotFound($"Author with {authorId} is not found !");
            }
            var getAuthorFromRepo = _libraryRepository.GetAuthor(authorId);
            _libraryRepository.DeleteAuthor(getAuthorFromRepo);
            if (!_libraryRepository.Save())
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
            if (!_libraryRepository.AuthorExists(authorId))
            {
                return BadRequest();
            }
            var authorFromRepo = _libraryRepository.GetAuthor(authorId);
            _mapper.Map(author, authorFromRepo);
            if (!_libraryRepository.Save())
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}