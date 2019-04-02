using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Books.API.Filters;
using Books.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IBookRepository _bookRepo;
        private IMapper _mapper;

        public BooksController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepo = bookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        //[BooksResultFilter]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookRepo.GetBooksAsync();
            //return Ok(books);
            return Ok(_mapper.Map<IEnumerable<ModelsDTO.Book>>(books));
        }

        [HttpGet]
        [Route("{id}")]
        //[BookResultFilter]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            var book = await _bookRepo.GetBookByIdAsync(id);
            if (book == null) 
                return NotFound();
            //return Ok(book);
            return Ok(_mapper.Map<ModelsDTO.Book>(book));
        }
    }
}
