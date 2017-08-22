using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationLibrary.Data.Entities
{
    public partial class Department
    {
        public Department()
        {
            Employee = new HashSet<Employee>();
        }

        public int DepartmentId { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Location { get; set; }

        [InverseProperty("Department")]
        public virtual ICollection<Employee> Employee { get; set; }
    }
}
