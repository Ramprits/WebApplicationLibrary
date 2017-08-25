using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationLibrary.Data.Entities
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }

        [Required, Column(TypeName = "varchar(50)")]
        public string Name { get; set; }
        public int? DepartmentId { get; set; }

        [ForeignKey("DepartmentId"), InverseProperty("Employee")]
        public virtual Department Department { get; set; }
    }
}
