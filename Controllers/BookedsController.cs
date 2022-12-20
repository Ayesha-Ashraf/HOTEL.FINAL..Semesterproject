using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HOTEL1.Data;
using HOTEL1.Models;
using Microsoft.AspNetCore.Authorization;


namespace HOTEL1.Controllers
{
    public class BookedsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookedsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bookeds
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Booked.Include(b => b.Hotels).Include(b => b.Rooms).Include(b => b.Users);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Bookeds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booked = await _context.Booked
                .Include(b => b.Hotels)
                .Include(b => b.Rooms)
                .Include(b => b.Users)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booked == null)
            {
                return NotFound();
            }

            return View(booked);
        }

        // GET: Bookeds/Create
        // public IActionResult Create()
        // {
        //    ViewData["HotelId"] = new SelectList(_context.Hotel, "Id", "Id");
        //   ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Id");
        //   return View();
        // }

        // POST: Bookeds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quantity,Status,LastUpdated,HotelId,RoomId,UserId")] Booked booked)
        {
            booked.Status = BookedState.InCart;
            booked.LastUpdated = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(booked);
                await _context.SaveChangesAsync();
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        // GET: Bookeds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booked = await _context.Booked.FindAsync(id);
            if (booked == null)
            {
                return NotFound();
            }
            ViewData["HotelId"] = new SelectList(_context.Hotel, "Id", "Title", booked.HotelId);
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Name", booked.RoomId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", booked.UserId);
            return View(booked);
        }

        // POST: Bookeds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Quantity,Status,LastUpdated,HotelId,RoomId,UserId")] Booked booked)
        {
            if (id != booked.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                booked.LastUpdated = DateTime.Now;
                try
                {
                    _context.Update(booked);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookedExists(booked.Id))
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
            ViewData["HotelId"] = new SelectList(_context.Hotel, "Id", "Id", booked.HotelId);
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Id", booked.RoomId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", booked.UserId);
            return View(booked);
        }


        

     
        public async Task<IActionResult> Confirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booked = await _context.Booked
                .Include(b => b.Hotels)
                .Include(b => b.Rooms)
                .Include(b => b.Users)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booked == null)
            {
                return NotFound();
            }
            booked.Status = BookedState.OrderPlaced;
            booked.LastUpdated = DateTime.Now;
            _context.Update(booked);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booked = await _context.Booked
                .Include(b => b.Hotels)
                 .Include(b => b.Rooms)
                .Include(b => b.Users)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booked == null)
            {
                return NotFound();
            }
            booked.Status = BookedState.OrderPlaced;

            return View(booked);
        }

            // POST: /Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var booked = await _context.Booked.FindAsync(id);
                _context.Booked.Remove(booked);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool BookedExists(int id)
            {
                return _context.Booked.Any(e => e.Id == id);
            }
        }
    }







