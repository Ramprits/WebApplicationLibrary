using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationLibrary.Entities
{
    [Table("Employee", Schema = "mst")]
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;

        [ForeignKey("DepartmentId")]
        public Department Departments { get; set; }

        public Guid DepartmentId { get; set; }
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }
}
