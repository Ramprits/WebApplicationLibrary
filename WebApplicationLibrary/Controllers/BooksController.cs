using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplicationLibrary.Services;
using AutoMapper;
using WebApplicationLibrary.Models;
using WebApplicationLibrary.Entities;
using WebApplicationLibrary.Filter;

namespace WebApplicationLibrary.Controllers
{
    [Produces("application/json"), Route("api/Authors/{authorId}/Books"), ValidateModel]
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
                return NotFound();
            if (!_repo.BookExists(bookId))
                return NotFound($"Book with {bookId} not found !");
            var getBooksForAuthor = _repo.GetBookForAuthor(authorId, bookId);
            if (getBooksForAuthor == null) throw new ArgumentNullException(nameof(getBooksForAuthor));
            return Ok(_mapper.Map<BookDto>(getBooksForAuthor));
        }

        [HttpPost]
        public IActionResult Post(Guid authorId, [FromBody] BookForCreationDto book)
        {
            if (book == null) return BadRequest();
            if (!_repo.AuthorExists(authorId)) return NotFound();
            if (book.Title == book.Description)
            {
                ModelState.AddModelError(nameof(BookForCreationDto),
                    "The provided description should be different from title .");
            }
            var bookEntity = _mapper.Map<Book>(book);
            _repo.AddBookForAuthor(authorId, bookEntity);
            if (ModelState.IsValid)
            {
                if (!_repo.Save()) throw new Exception($"Creating book for {authorId} field not save !");
            }
            var bookToReturn = _mapper.Map<BookDto>(bookEntity);
            return Created("GetBookForAuthor", bookToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid authorId, Guid id)
        {
            if (!_repo.AuthorExists(authorId))
            {
                return NotFound($"Author with {authorId} not found !");
            }
            var bookForAuthorFromRepo = _repo.GetBookForAuthor(authorId, id);
            if (bookForAuthorFromRepo == null)
            {
                return NotFound();
            }
            _repo.DeleteBook(bookForAuthorFromRepo);

            if (!_repo.Save())
            {
                return BadRequest($"Book delete is not sucessfully");
            }
            return NoContent();
        }

        [HttpPut("{bookId}")]
        public IActionResult Put(Guid authorId, Guid bookId, [FromBody] BookForUpdateDto book)
        {
            if (!_repo.AuthorExists(authorId))
            {
                return NotFound();
            }

            var bookFromRepo = _repo.GetBookForAuthor(authorId, bookId);
            if (bookFromRepo == null)
            {
                return NotFound($"update with {bookId} and with {authorId} doest save");
            }
            _mapper.Map(book, bookFromRepo);
            _repo.UpdateBookForAuthor(bookFromRepo);
            if (!_repo.Save())
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}