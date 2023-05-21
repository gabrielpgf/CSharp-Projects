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
            var newEvent = new Event(form, _context.Locations.FirstOrDefault(x => x.Name == form["Location"]));
            _context.Add(newEvent);
            _context.SaveChanges();
        }

        public void CreateLocation(Location location)
        {
            _context.Add(location);
            _context.SaveChanges();
        }

        public void DeleteEvent(int id)
        {
            var myEvent = _context.Events.Find(id);
            if (myEvent != null)
            {
                _context.Remove(myEvent);
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
            var myEvent = _context.Events.FirstOrDefault(p => p.Id == form["Id"]);
            //CÓDIGO A SER TESTADO DEPOIS
            /*if (myEvent != null)
            {
                myEvent = new Event(form, _context.Locations.FirstOrDefault(x => x.Name == form["Location"]));
                _context.Update(myEvent);
                _context.SaveChanges();
            }*/
            var location = _context.Locations.FirstOrDefault(p => p.Name == form["Location"]);
            myEvent.UpdateEvent(form, location);
            _context.Entry(myEvent).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
