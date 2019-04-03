using System;
using AutoMapper;

namespace Books.API
{
    // good practice to use mapping profiles for big projects
    public class BooksProfile : Profile
    {
        public BooksProfile()
        {
            // create map where author object in source is mapped to a string in DTO by concatenating first and last name
            CreateMap<Entities.Book, ModelsDTO.Book>().
                ForMember(destinationMember => destinationMember.Author, memberOptions =>
                    memberOptions.MapFrom(source => $"{source.Author.FirstName} {source.Author.LastName}"));

            CreateMap<ModelsDTO.BookForHttpPost, Entities.Book>();
        }

    }
}
