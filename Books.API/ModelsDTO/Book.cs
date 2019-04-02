using System;
namespace Books.API.ModelsDTO
{
    public class Book
    {
        public Guid Id { get; set; }
        //concat of first and last name
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}
