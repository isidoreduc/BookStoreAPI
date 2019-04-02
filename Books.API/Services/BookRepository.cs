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

        public BookRepository(BooksContext booksContext)
        {
            _booksContext = booksContext;
        }

        public async Task<Book> GetBookByIdAsync(Guid Id)
        {
            return await _booksContext.Books.Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == Id);
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _booksContext.Books
                 .Include(b => b.Author).ToListAsync();
        }
    }
}
