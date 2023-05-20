using Microsoft.AspNetCore.Identity;

namespace Calendar.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Event> Events { get; set; }
    }
}
