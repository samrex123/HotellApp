#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelSite1.Models;
using Microsoft.AspNetCore.Authorization;

namespace HotelSite1.Controllers
{
    public class BookingsController : Controller
    {
        private readonly HotellAppContext _context;

        public BookingsController(HotellAppContext context)
        {
            _context = context;
        }

        // GET: Bookings
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            var hotellAppContext = _context.Bookings.Include(b => b.Customers);
            return View(await hotellAppContext.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Customers)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        [Authorize]
        public IActionResult Create()
        {
            
            //ViewData["Customersid"] = new SelectList(_context.Customers, "Id", "Id");
            return View(); 

        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(short id, [Bind("Qtypersons,Startdate,Enddate,Eta,Timearrival,Timedeparture,Specialneeds,Extrabed")] Booking booking)
        {
            var userEmail = User.Identity.Name;
            var customersList = _context.Customers.ToList();
            foreach (var customerr in customersList)
            {
                if (customerr.Email == userEmail)
                {
                    var customer = await _context.Customers.FirstOrDefaultAsync(m => m.Email == userEmail);

                    booking.Roomsid = id;
                    //var chosenRoom = booking.Rooms.FirstOrDefault(m => m.Id == id);
                    booking.Customersid = customer.Id;
                    booking.Customers = await _context.Customers.FirstOrDefaultAsync(m =>m.Id == customer.Id);
                }
            }

            bool condition1 = true;
            bool condition2 = true;
            bool condition3 = true;

            foreach (var existingBooking in _context.Bookings)
            {
                if (booking.Startdate < existingBooking.Startdate && booking.Enddate > existingBooking.Enddate)
                {
                    condition1 = false;
                    break;
                }

                if (existingBooking.Startdate <= booking.Enddate && booking.Enddate < existingBooking.Enddate)
                {
                    condition2 = false;
                    break;
                }

                if (existingBooking.Startdate <= booking.Startdate && booking.Startdate < existingBooking.Enddate)
                {
                    condition3 = false;
                    break;
                }
            }
            if (condition1 && condition2 && condition3)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(booking);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                ViewBag.ErrorMessage = "The room is not available for these dates";
            }
            //ViewData["Customersid"] = new SelectList(_context.Customers, "Id", "Id", booking.Customersid);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["Customersid"] = new SelectList(_context.Customers, "Id", "Id", booking.Customersid);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Customersid,Qtypersons,Startdate,Enddate,Eta,Timearrival,Timedeparture,Specialneeds,Extrabed")] Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Customersid"] = new SelectList(_context.Customers, "Id", "Id", booking.Customersid);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Customers)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(long id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}
