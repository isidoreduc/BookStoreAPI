using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Books.API.Contexts;
using Books.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Books.API.Services
{
    public class BookRepository : IBookRepository
    {
        private BooksContext _booksContext;

        public BookRepository(BooksContext booksContext) =>
            _booksContext = booksContext;

        public async Task<Book> GetBookByIdAsync(Guid Id) =>
            await _booksContext.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == Id);

        public async Task<IEnumerable<Book>> GetBooksAsync() =>
            await _booksContext.Books.Include(b => b.Author).ToListAsync();

        // no sense to make it async, as there is no wire communication with database, it is just added to DbSet in the context
        public void CreateBook(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            _booksContext.Add(book);
        }
               
        // we want to persist the changes made to context (adding a book for example), so this is 
        // database interaction, so async makes sense
        public async Task<bool> SaveChangesAsync() => 
            await _booksContext.SaveChangesAsync() > 0; // if at least one change

    }
}
