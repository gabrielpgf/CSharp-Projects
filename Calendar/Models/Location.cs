using System.ComponentModel.DataAnnotations;

namespace Calendar.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        //Relatioal data
        public virtual ICollection<Event> Events { get; set; }
    }
}
