using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Books.API.Filters;
using Books.API.ModelsDTO;
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
        [Route("{id}", Name = "GetBook")]
        //[BookResultFilter]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            var book = await _bookRepo.GetBookByIdAsync(id);
            if (book == null) 
                return NotFound();
            //return Ok(book);
            return Ok(_mapper.Map<ModelsDTO.Book>(book));
        }

        [HttpPost]
        //[BookResultFilter]
        public async Task<IActionResult> CreateBook([FromBody]BookForHttpPost book) // FromBody - model binding attribute
        {
            // need to create a mapping in BooksProfile from Entity Book to BookForHttpPost
            var mappedBook = _mapper.Map<Entities.Book>(book);
            _bookRepo.CreateBook(mappedBook);
            // persist changes
            await _bookRepo.SaveChangesAsync();
            // invoking the newly created book to have access to it in the context (for author info mainly)
            await _bookRepo.GetBookByIdAsync(mappedBook.Id);
            // responding with a 201 Created Status
            return CreatedAtRoute("GetBook", new { id = mappedBook.Id }, mappedBook);
        }
    }
}
