using System.ComponentModel.DataAnnotations;

namespace Calendar.Models
{
    public class Event
    {
        [Key]
        public int Íd { get; set; }
        public string Name { get; set; }
        public string  Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        //Relation Data
        public virtual Location Location { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}
