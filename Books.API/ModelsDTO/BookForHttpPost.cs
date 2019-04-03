using System;
namespace Books.API.ModelsDTO
{
    public class BookForHttpPost
    {
        // no book Id property, it's server's/API's job to award an id, not the user's
        public Guid AuthorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
