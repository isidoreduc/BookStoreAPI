using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Books.API.Entities;

namespace Books.API.Services
{
    public interface IBookRepository
    {
        // synchronous
        //IEnumerable<Book> GetBooks();
        //Book GetBookById(Guid Id);

        // asynchronous
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<Book> GetBookByIdAsync(Guid Id);
    }
}
