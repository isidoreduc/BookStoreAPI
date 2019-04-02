using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Books.API.Filters
{
    public class BookResultFilter : ResultFilterAttribute
    {
        public BookResultFilter()
        {
        }
    }
}
