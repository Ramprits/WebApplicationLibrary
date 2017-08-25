using System;

namespace WebApplicationLibrary.Models
{
    public class AuthorForUpdateDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTimeOffset DateOfBirth { get; set; } = DateTimeOffset.Now;

        public string Genre { get; set; }
    }
}
