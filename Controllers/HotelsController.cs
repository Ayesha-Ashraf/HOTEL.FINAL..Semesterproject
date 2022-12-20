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
    public class HotelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IEnumerable<int> Customers;

        public HotelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hotels
        public async Task<IActionResult> Index()
        {
            ViewData["CustomerofHotel"] = await _context.CustomerofHotel.ToListAsync();
            ViewData["Customers"] = await _context.Customer.ToListAsync();
            var applicationDbContext = _context.Hotel.Include(b => b.Rooms);
            return View(await applicationDbContext.ToListAsync());
        }
        public IActionResult SearchForm()
        {
            return View();
        }
        public async Task<IActionResult> HotelList()
        {
            return View(await _context.Hotel.ToListAsync());
        }
        public async Task<IActionResult> SearchResult(string Title)
        {
            ViewData["CustomerofHotel"] = await _context.CustomerofHotel.ToListAsync();
            ViewData["Customers"] = await _context.Customer.ToListAsync();
            var applicationDbContext = _context.Hotel.Include(f => f.Rooms);
            return View("Index", await _context.Hotel.Where(a => a.Title.Contains(Title)).ToListAsync());
        }
        public async Task<IActionResult> HotelDetails(int? Id)
        {
            if(Id == null)
            {
                return NotFound();
            }
            ViewData["CustomerofHotel"] = await _context.CustomerofHotel.ToListAsync();
            ViewData["Customers"] = await _context.Customer.ToListAsync();
            ViewData["Rooms"] = await _context.Room.ToListAsync();

            return View("Index", await _context.Hotel.Where(a => a.Id == Id).ToListAsync());
        }
     

        // GET: Hotels/Create
        [Authorize(Roles = "Admin")]
        
        public IActionResult Create()
        {
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Name");
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Name");
            return View();
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,URL,Price,Address,location,RoomId")] Hotel hotel, List<int> Customers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotel);
                await _context.SaveChangesAsync();
                List<CustomerofHotel> customerofHotel = new List<CustomerofHotel>();
                foreach (int customer in Customers)
                {
                    customerofHotel.Add(new CustomerofHotel { CustomerId = customer, HotelId = hotel.Id });
                }
                _context.CustomerofHotel.AddRange(customerofHotel);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Id", hotel.RoomId);
            return View(hotel);
        }

        // GET: Hotels/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotel.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }
            IList<CustomerofHotel> customerofHotels = await _context.CustomerofHotel.Where<CustomerofHotel>(a => a.HotelId == hotel.Id).ToListAsync();
            IList<int> listCustomers = new List<int>();
            foreach (CustomerofHotel customerofHotel in customerofHotels)
            {
                listCustomers.Add(customerofHotel.CustomerId);
            }



            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Name", hotel.RoomId);
            ViewData["CustomerId"] = new MultiSelectList(_context.Customer, "Id", "Name", listCustomers.ToArray());
            return View(hotel);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,URL,Price,Address,location,RoomId")] Hotel hotel)
        {
            var transaction = _context.Database.BeginTransaction();

            if (id != hotel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotel);
                    _context.SaveChanges();
                    List<CustomerofHotel> customerofHotel= new List<CustomerofHotel>();
                    foreach (int customer in Customers)
                    {
                        customerofHotel.Add(new CustomerofHotel { CustomerId = customer, HotelId = hotel.Id });
                    }
                    List<CustomerofHotel> deleteCustomerofHotel = await _context.CustomerofHotel.Where<CustomerofHotel>(a => a.HotelId == hotel.Id).ToListAsync();
                    _context.CustomerofHotel.RemoveRange(deleteCustomerofHotel);
                    _context.SaveChanges();

                    _context.CustomerofHotel.UpdateRange(customerofHotel);
                    _context.SaveChanges();

                    await transaction.CommitAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelExists(hotel.Id))
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
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Id", hotel.RoomId);
            return View(hotel);
        }

        // GET: Hotels/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotel
                .Include(b => b.Rooms)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        // POST: Hotels/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hotel = await _context.Hotel.FindAsync(id);
            _context.Hotel.Remove(hotel);
             _context.SaveChanges();
            List<CustomerofHotel> deleteCustomerofHotel = await _context.CustomerofHotel.Where<CustomerofHotel>(a => a.HotelId == hotel.Id).ToListAsync();
            _context.CustomerofHotel.RemoveRange(deleteCustomerofHotel);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        private bool HotelExists(int id)
        {
            return _context.Hotel.Any(e => e.Id == id);
        }
    }
}
