using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Books.API.Entities;
using Books.API.ExternalModels;

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
        void CreateBook(Book book);
        Task<BookCover> GetBookCoverByIdAsync(string coverId);
        Task<bool> SaveChangesAsync();

    }
}
