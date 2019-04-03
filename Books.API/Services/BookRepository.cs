using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Books.API.Contexts;
using Books.API.Entities;
using Books.API.ExternalModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Books.API.Services
{
    public class BookRepository : IBookRepository
    {
        private BooksContext _booksContext;
        private IHttpClientFactory _httpClient;

        public BookRepository(BooksContext booksContext, IHttpClientFactory httpClientFactory)
        {
            _booksContext = booksContext ?? throw new ArgumentNullException(nameof(booksContext));
            _httpClient = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }
       

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

        // using async to interface with an external API (BookCovers.API)
        // need to inject an http client factory to access API
        public async Task<BookCover> GetBookCoverByIdAsync(string coverId)
        {
            var client = _httpClient.CreateClient();
            var response = await client.GetAsync($"http://localhost:52644/api/bookcovers/{coverId}");
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<BookCover>(await response.Content.ReadAsStringAsync());
            return null; 
        }
    }
}
