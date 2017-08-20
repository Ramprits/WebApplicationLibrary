using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationLibrary.Entities;
using WebApplicationLibrary.Models;

namespace WebApplicationLibrary.Helpers
{
    public class Profiler : Profile
    {
        public Profiler()
        {
            CreateMap<Author, Models.AuthorDto>()
                .ForMember(des => des.Name, opt => opt.MapFrom(x => $"{x.FirstName } {x.LastName}")).
                ForMember(dest => dest.Age, opt => opt.MapFrom(x => x.DateOfBirth.GetCurrentAge()))
                .ReverseMap();

            CreateMap<Department, DepartmentDto>().ReverseMap();

            CreateMap<Employee, EmployeeDto>().ReverseMap();

            CreateMap<Book, Models.BookDto>().ReverseMap();
            // Create Author
            CreateMap<AuthorForCreationDto, Author>().ReverseMap();
            //Update Author
            CreateMap<AuthorForUpdateDto, Author>().ReverseMap();

            CreateMap<BookForCreationDto, Book>().ReverseMap();

            CreateMap<BookForUpdateDto, Book>().ReverseMap();
        }
    }
}
