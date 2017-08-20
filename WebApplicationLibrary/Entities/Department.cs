using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationLibrary.Entities
{
    [Table("Department", Schema = "mst")]
    public class Department
    {
        [Key]
        public Guid DepartmentId { get; set; } 

        public string Name { get; set; }

        public string Location { get; set; }

        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();

    }
}
