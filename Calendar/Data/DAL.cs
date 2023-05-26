using Calendar.Models;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Data
{
    public interface IDAL
    {
        public List<Event> GetEvents();
        public List<Event> GetMyEvents(string userId);
        public Event GetEvent(int id);
        public void CreateEvent(IFormCollection form);
        public void UpdateEvent(IFormCollection form);
        public void DeleteEvent(int id);
        public List<Location> GetLocations();
        public Location GetLocation(int id);
        public void CreateLocation(Location location);
    }
    public class DAL : IDAL
    {
        private readonly ApplicationDbContext _context;

        public DAL(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateEvent(IFormCollection form)
        {
            var locName = form["Location"].ToString();
            var user = _context.Users.FirstOrDefault(x => x.Id == form["UserId"].ToString());
            var newEvent = new Event(form, _context.Locations.FirstOrDefault(x => x.Name == locName), user);
            _context.Events.Add(newEvent);
            _context.SaveChanges();
        }

        public void CreateLocation(Location location)
        {
            _context.Locations.Add(location);
            _context.SaveChanges();
        }

        public void DeleteEvent(int id)
        {
            var myEvent = _context.Events.Find(id);
            if (myEvent != null)
            {
                _context.Events.Remove(myEvent);
                _context.SaveChanges();
            }
        }

        public Event GetEvent(int id)
        {
            return _context.Events.FirstOrDefault(x => x.Id == id);
        }

        public List<Event> GetEvents()
        {
            return _context.Events.ToList();
        }

        public Location GetLocation(int id)
        {
            return _context.Locations.Find(id);
        }

        public List<Location> GetLocations()
        {
            return _context.Locations.ToList();
        }

        public List<Event> GetMyEvents(string userId)
        {
            return _context.Events.Where(p => p.User.Id == userId).ToList();
        }

        public void UpdateEvent(IFormCollection form)
        {
            var myEvent = _context.Events.FirstOrDefault(p => p.Id == int.Parse(form["Event.Id"])); 
            var locName = form["Location"].ToString();
            var user = _context.Users.FirstOrDefault(p => p.Id == form["UserId"].ToString());
            var location = _context.Locations.FirstOrDefault(p => p.Name == locName);
            myEvent.UpdateEvent(form, location, user);
            _context.Entry(myEvent).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
