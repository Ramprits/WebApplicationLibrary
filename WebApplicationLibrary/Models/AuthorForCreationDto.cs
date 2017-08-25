using System;
using WebApplicationLibrary.Entities;

namespace WebApplicationLibrary.Models
{
    public class AuthorForCreationDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTimeOffset DateOfBirth { get; set; } = DateTimeOffset.Now;

        public string Genre { get; set; }

        public CampUser  UserName { get; set; }
    }
}
