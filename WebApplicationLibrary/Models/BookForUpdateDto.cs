using System.ComponentModel.DataAnnotations;

namespace WebApplicationLibrary.Models
{
    public class BookForUpdateDto : BookForManipulationDto
    {
        [Required(ErrorMessage = "You should fill out a Description.")]
        public override string Description { get => base.Description; set => base.Description = value; }
    }
}
