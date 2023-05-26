using Microsoft.AspNetCore.Mvc.TagHelpers;
using Newtonsoft.Json;
using System.Text.Json;

namespace Calendar.Helpers
{
    public static class JSONListHelper
    {
        public static string GetEventListJSONString(List<Models.Event> events)
        {
            var eventList = new List<Event>();            
            foreach (var model in events)
            {
                var myEvent = new Event()
                {
                    id = model.Id,
                    title = model.Name,
                    start = model.StartTime,
                    end = model.EndTime,
                    resourceId = model.Location.Id,
                    description = model.Description                    
                };

                eventList.Add(myEvent);
            }
            return System.Text.Json.JsonSerializer.Serialize(eventList);
        }

        public static string GetResourceListJSONString(List<Models.Location> locations)
        {
            var resourceList = new List<Resource>();
            foreach (var model in locations)
            {
                var myLocation = new Resource()
                {
                    id = model.Id,
                    title = model.Name
                };
                resourceList.Add(myLocation);
            }
            return System.Text.Json.JsonSerializer.Serialize(resourceList);
        }
    }

    public class Event
    {
        public int id { get; set; }
        /*[JsonProperty("title")]*/
        public string title { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public int resourceId { get; set; }
        public string description { get; set; }
    }

    public class Resource
    {
        public int id { get; set; }
        public string title { get; set; }
    }
}
