using Microsoft.AspNetCore.Mvc.Rendering;

namespace Calendar.Models.ViewModels
{
    public class EventViewModel
    {
        public Event Event { get; set; }
        public List<SelectListItem> Location = new List<SelectListItem>();
        public string LocationName { get; set; }

        public EventViewModel(Event myevent, List<Location> locations)
        {
            Event = myevent;
            LocationName = myevent.Location.Name;
            foreach (var location in locations)
            {
                Location.Add(new SelectListItem { Text = location.Name });
            }
        }

        public EventViewModel(List<Location> locations)
        {
            foreach (var location in locations)
            {
                Location.Add(new SelectListItem { Text = location.Name });
            }
        }

        public EventViewModel() { }
    }
}
