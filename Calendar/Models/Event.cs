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

        public Event(IFormCollection form, Location location, ApplicationUser user)
        {  
            Name = form["Event.Name"].ToString();
            Description = form["Event.Description"].ToString();
            StartTime = DateTime.Parse(form["Event.StartTime"].ToString());
            EndTime = DateTime.Parse(form["Event.EndTime"].ToString());
            Location = location;
            User = user;
        }


        public void UpdateEvent(IFormCollection form, Location location, ApplicationUser user)
        {
            Name = form["Event.Name"].ToString();
            Description = form["Event.Description"].ToString();
            StartTime = DateTime.Parse(form["Event.StartTime"].ToString());
            EndTime = DateTime.Parse(form["Event.EndTime"].ToString());
            Location = location;
            User = user;
        }
    }
}
