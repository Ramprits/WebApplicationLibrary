using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationLibrary.Models
{
    public class DepartmentDto
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;

    }
}
