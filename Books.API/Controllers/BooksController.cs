using System;
using System.Threading.Tasks;
using Books.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IBookRepository _bookRepo;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepo = bookRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookRepo.GetBooksAsync();
            return Ok(books);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            var book = await _bookRepo.GetBookByIdAsync(id);
            if (book == null) 
                return NotFound();
            return Ok(book);
        }
    }
}
