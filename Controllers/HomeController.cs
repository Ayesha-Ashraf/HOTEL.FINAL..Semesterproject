using HOTEL1.Data;
using HOTEL1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HOTEL1.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly ApplicationDbContext _context;
        public HomeController( ApplicationDbContext context)
        {
           
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            ViewData["Hotels"] = await _context.Hotel.ToListAsync<Hotel>();
            ViewData["Rooms"] = await _context.Room.ToListAsync<Room>();
            return View();

        }
        public async Task<IActionResult> Dinning()
        {

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
