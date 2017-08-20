using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationLibrary.Entities
{
    [Table("Employee", Schema = "mst")]
    public class Employee
    {
        public Guid EmployeeId { get; set; } = new Guid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }

        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;

        [ForeignKey("DepartmentId")]
        public Guid DepartmentId { get; set; }
        public Department Departments { get; set; }
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }
}
