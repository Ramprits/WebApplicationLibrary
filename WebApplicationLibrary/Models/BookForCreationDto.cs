using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationLibrary.Models
{
    public class BookForCreationDto : BookForManipulationDto
    {
        public Guid AuthorId { get; set; }
    }
}
