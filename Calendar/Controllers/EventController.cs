﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Calendar.Data;
using Calendar.Models;
using Calendar.Models.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Calendar.Controllers.ActionFilters;

namespace Calendar.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly IDAL _idal;
        private readonly UserManager<ApplicationUser> _usermanager;

        public EventController(IDAL idal, UserManager<ApplicationUser> usermanager)
        {
            _idal = idal;
            _usermanager = usermanager;
        }

        // GET: Event
        public IActionResult Index()
        {
            if (TempData["Alert"] != null)
                ViewData["Alert"] = TempData["Alert"];
            return View(_idal.GetMyEvents(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }

        // GET: Event/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = _idal.GetEvent((int)id);

            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Event/Create
        public IActionResult Create()
        {
            return View(new EventViewModel(_idal.GetLocations(), User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }

        // POST: Event/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EventViewModel vm, IFormCollection form)
        {
            try
            {
                _idal.CreateEvent(form);
                TempData["Alert"] = "Sucess! You created a new event for: " + form["Event.Name"];
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Alert"] = "An error occurred: " + ex.Message;
                return View(vm);
            }

        }

        // GET: Event/Edit/5
        [UserAccessOnly]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = _idal.GetEvent((int)id);
            if (@event == null)
            {
                return NotFound();
            }
            var vm = new EventViewModel(@event, _idal.GetLocations(), User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(vm);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection form)
        {
            try
            {
                _idal.UpdateEvent(form);
                TempData["Alert"] = "Sucess! You modified an event for: " + form["Event.Name"];
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Alert"] = "An error occurred: " + ex.Message;
                var vm = new EventViewModel(_idal.GetEvent(id), _idal.GetLocations(), User.FindFirstValue(ClaimTypes.NameIdentifier));
                return View(vm);
            }
        }

        // GET: Event/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = _idal.GetEvent((int)id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _idal.DeleteEvent(id);
            TempData["Alert"] = "You deleted an event";
            return RedirectToAction(nameof(Index));
        }        
    }
}
