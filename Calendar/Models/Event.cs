using System.ComponentModel.DataAnnotations;

namespace Calendar.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string  Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        //Relation Data
        public virtual Location Location { get; set; }
        public virtual ApplicationUser User { get; set; }

        public Event() { }
        public Event(IFormCollection form, Location location) 
        {
            Id = int.Parse(form["Id"]);
            Name = form["Name"];
            Description = form["Description"];
            StartTime = DateTime.Parse(form["StartTime"]);
            EndTime = DateTime.Parse(form["EndTime"]);
            Location = location;
        }


        public void UpdateEvent(IFormCollection form, Location location)
        {
            Id = int.Parse(form["Id"]);
            Name = form["Name"];
            Description = form["Description"];
            StartTime = DateTime.Parse(form["StartTime"]);
            EndTime = DateTime.Parse(form["EndTime"]);
            Location = location;
        }
    }
}
