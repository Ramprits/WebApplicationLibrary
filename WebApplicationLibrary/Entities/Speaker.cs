using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationLibrary.Entities
{
    public class Speaker
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string WebsiteUrl { get; set; }
        public string TwitterName { get; set; }
        public string GitHubName { get; set; }
        public string Bio { get; set; }
        public string HeadShotUrl { get; set; }
        public int UserId { get; set; }
        public CampUser User { get; set; }

        public ICollection<Talk> Talks { get; set; } = new List<Talk>();
        public int CampId { get; set; }
        [ForeignKey("CampId")]
        public Camp Camp { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}