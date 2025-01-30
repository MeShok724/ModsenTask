using AutoMapper;
using ModsenTask.Application.DTOs;
using ModsenTask.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenTask.Application.Mapping
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookResponse>();
            CreateMap<BookRequest, Book>();
        }
    }
}
