﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationLibrary.Data.Entities
{
    public partial class EmployeeTerritories
    {
        [Column("EmployeeID")]
        public int EmployeeId { get; set; }

        [Column("TerritoryID"), MaxLength(20)]
        public string TerritoryId { get; set; }

        [ForeignKey("EmployeeId"), InverseProperty("EmployeeTerritories")]
        public virtual Employees Employee { get; set; }

        [ForeignKey("TerritoryId"), InverseProperty("EmployeeTerritories")]
        public virtual Territories Territory { get; set; }
    }
}
