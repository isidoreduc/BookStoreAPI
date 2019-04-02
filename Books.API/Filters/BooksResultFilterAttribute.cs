using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Books.API.Filters
{
    public class BooksResultFilterAttribute : ResultFilterAttribute
    {
        //private IMapper _mapper;

        //public BooksResultFilter() { }

        //public BooksResultFilter(IMapper mapper) => _mapper = mapper;
        //public IMapper Mapper { get => _mapper; }
        // same as Book, this time for a collection of books

        public override async Task OnResultExecutionAsync(ResultExecutingContext context,
            ResultExecutionDelegate next)
        {
            var resultFromAction = context.Result as ObjectResult;
            if (resultFromAction?.Value == null
               || resultFromAction.StatusCode < 200
               || resultFromAction.StatusCode >= 300)
            {
                await next();
                return;
            }

            resultFromAction.Value = Mapper.Map<ModelsDTO.Book>(resultFromAction.Value);

            await next();
        }

    }

}
