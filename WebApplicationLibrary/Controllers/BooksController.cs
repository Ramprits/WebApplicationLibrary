using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplicationLibrary.Services;
using AutoMapper;
using WebApplicationLibrary.Models;
using WebApplicationLibrary.Entities;

namespace WebApplicationLibrary.Controllers
{
    [Produces("application/json")]
    [Route("api/Authors/{authorId}/Books")]
    public class BooksController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILibraryRepository _repo;

        public BooksController(IMapper mapper, ILibraryRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        [HttpGet()]
        public IActionResult Get(Guid authorId)
        {
            if (!_repo.AuthorExists(authorId))
            {
                return NotFound();
            }
            var getBooksForAuthor = _repo.GetBooksForAuthor(authorId);
            return Ok(_mapper.Map<IEnumerable<BookDto>>(getBooksForAuthor));
        }

        [HttpGet("{bookId}", Name = "GetBookForAuthor")]
        public IActionResult Get(Guid authorId, Guid bookId)
        {
            if (!_repo.AuthorExists(authorId))
            {
                return NotFound();
            }
            var getBooksForAuthor = _repo.GetBookForAuthor(authorId, bookId);
            return Ok(_mapper.Map<BookDto>(getBooksForAuthor));
        }

        [HttpPost]
        public IActionResult Post(Guid authorId, [FromBody] BookForCreationDto book)
        {
            if (book == null) return BadRequest();
            if (!_repo.AuthorExists(authorId)) return NotFound();
            var bookEntity = _mapper.Map<Book>(book);
            _repo.AddBookForAuthor(authorId, bookEntity);
            if (!_repo.Save()) throw new Exception($"Creating book for {authorId} field not save !");
            var bookToReturn = _mapper.Map<BookDto>(bookEntity);
            return Created("GetBookForAuthor", bookToReturn);
        }


    }
}