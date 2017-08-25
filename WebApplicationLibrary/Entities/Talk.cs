using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationLibrary.Entities
{
    public class Talk
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Category { get; set; }
        public string Level { get; set; }
        public string Prerequisites { get; set; }
        public DateTime StartingTime { get; set; } = DateTime.Now;
        public string Room { get; set; }
        public int SpeakerId { get; set; }
        [ForeignKey("SpeakerId")]
        public Speaker Speaker { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}