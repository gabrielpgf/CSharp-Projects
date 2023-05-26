using Calendar.Data;
using Calendar.Helpers;
using Calendar.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Diagnostics;
using System.Security.Claims;

namespace Calendar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDAL _idal;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, IDAL idal, UserManager<ApplicationUser> usermanager)
        {
            _logger = logger;
            _idal = idal;
            _userManager = usermanager;
        }

        public IActionResult Index()
        {
            ViewData["Resources"] = JSONListHelper.GetResourceListJSONString(_idal.GetLocations());
            ViewData["Events"] = JSONListHelper.GetEventListJSONString(_idal.GetEvents());
            return View();
        }

        [Authorize]
        public IActionResult MyCalendar()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["Resources"] = JSONListHelper.GetResourceListJSONString(_idal.GetLocations());
            ViewData["Events"] = JSONListHelper.GetEventListJSONString(_idal.GetMyEvents(userId));
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}