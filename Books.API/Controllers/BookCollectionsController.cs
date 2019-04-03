using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Books.API.ModelsDTO;
using Books.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers
{
    [Route("api/bookscollection")]
    [ApiController]
    // for adding books in bulk (more than 1)
    public class BookCollectionsController : ControllerBase
    {
        private IBookRepository _booksRepo;
        private IMapper _mapper;

        public BookCollectionsController(IBookRepository booksRepository, IMapper mapper)
        {
            _booksRepo = booksRepository ?? throw new ArgumentNullException(nameof(booksRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<IActionResult> CreateBookCollection([FromBody] IEnumerable<BookForHttpPost> books)
        {
            var mappedBooks = _mapper.Map<IEnumerable<Entities.Book>>(books);
            foreach (var item in mappedBooks)
                _booksRepo.CreateBook(item);
            await _booksRepo.SaveChangesAsync();
            return Ok();
        }
    }
}
