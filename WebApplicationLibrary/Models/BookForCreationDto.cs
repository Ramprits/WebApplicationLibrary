using System;

namespace WebApplicationLibrary.Models
{
    public class BookForCreationDto : BookForManipulationDto
    {
        public Guid AuthorId { get; set; }
    }
}
