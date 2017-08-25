using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationLibrary.Data.Entities
{
    public partial class CustomerCustomerDemo
    {
        [Column("CustomerID", TypeName = "nchar(5)")]
        public string CustomerId { get; set; }

        [Column("CustomerTypeID", TypeName = "nchar(10)")]
        public string CustomerTypeId { get; set; }

        [ForeignKey("CustomerTypeId"), InverseProperty("CustomerCustomerDemo")]
        public virtual CustomerDemographics CustomerType { get; set; }
    }
}
