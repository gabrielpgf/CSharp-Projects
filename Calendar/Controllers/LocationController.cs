using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Calendar.Data;
using Calendar.Models;

namespace Calendar.Controllers
{
    public class LocationController : Controller
    {

        private readonly IDAL _idal;

        public LocationController(IDAL idal)
        {
            _idal = idal;
        }

        // GET: Location
        public IActionResult Index()
        {
            if (TempData["Alert"] != null)
                TempData["Alert"] = TempData["Alert"];
            return View(_idal.GetLocations());
        }

        // GET: Location/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = _idal.GetLocation((int)id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // GET: Location/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Location/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name")] Location location)
        {

            try
            {
                _idal.CreateLocation(location);
                TempData["Alert"] = $"Location {location.Name} created.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData["Alert"] = "An error occurred " + ex.Message;
                return View(location);
            }


        }
    }
}
