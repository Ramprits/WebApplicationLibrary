using System;

namespace WebApplicationLibrary.Models
{
    public class DepartmentDto
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;

    }
}
