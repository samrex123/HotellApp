﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelSite1.Models;

namespace HotelSite1.Controllers
{
    public class CustomersController : Controller
    {
        private readonly HotellAppContext _context;

        public CustomersController(HotellAppContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var hotellAppContext = _context.Customers.Include(c => c.Customertypes);
            return View(await hotellAppContext.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Customertypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Details/5
        public async Task<ActionResult> MyInfo()
        {

            var userEmail = User.Identity.Name;
            var customersList = _context.Customers.ToList();
            foreach (var customerr in customersList)
            {
                if (customerr.Email == userEmail)
                {
                    var customer = await _context.Customers.FirstOrDefaultAsync(m => m.Email == userEmail);
                    
                    return RedirectToAction(nameof(Details) +$"/{customer.Id}");

                }
                //else
                //{
                //    return RedirectToAction(nameof(Create));
                //}
            }

            return RedirectToAction(nameof(Create));
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            ViewData["Customertypesid"] = new SelectList(_context.Customertypes, "Id", "Id");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Customertypesid,Firstname,Lastname,Email,Streetadress,City,Country,Phonenumber,Ice,Lastupdated")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Customertypesid"] = new SelectList(_context.Customertypes, "Id", "Id", customer.Customertypesid);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["Customertypesid"] = new SelectList(_context.Customertypes, "Id", "Id", customer.Customertypesid);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Customertypesid,Firstname,Lastname,Email,Streetadress,City,Country,Phonenumber,Ice,Lastupdated")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            ViewData["Customertypesid"] = new SelectList(_context.Customertypes, "Id", "Id", customer.Customertypesid);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Customertypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(long id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
