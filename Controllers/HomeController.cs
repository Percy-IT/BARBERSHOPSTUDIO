using BARBERSHOP_STUDIO.Models;
using BARBERSHOPSTUDIO.Data;
using BARBERSHOPSTUDIO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace BARBERSHOPSTUDIO.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Haircuts()
        {
            return View();
        }

        public IActionResult Hairproducts()
        {
            return View();
        }


        [HttpGet]
        public IActionResult BookAppointment()
        {
           ViewBag.Barbers = _context.Barbers.ToList(); 
            ViewBag.Services = new SelectList(_context.Services, "Id", "Name");
            ViewBag.Barbers = new SelectList(_context.Barbers, "BarberId", "Name");
            



            return View();
        }

         [HttpPost]

         public IActionResult BookAppointment(Appointment appointment)
         {

            var service = _context.Services.Find(appointment.Id);
            var cost = service.Price;

            // Save the appointment and cost to the database
            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            ViewBag.Cost = cost;
            



            if (ModelState.IsValid)
             {
                 _context.Appointments.Add(appointment);
                 _context.SaveChanges();
                 return RedirectToAction("Book Appointment");
             }
             ViewBag.Barbers = _context.Barbers.ToList();
            ViewBag.Services = _context.Services.ToList();
            
            return View(appointment);
         } 

        


            public IActionResult MyAppointments()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appointments = _context.Appointments
                .Where(a => a.Client.Email == userId)
                .Include(a => a.Barber)
                .ToList();
            return View(appointments);
        }



        
        public IActionResult CancelAppointment(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment != null) 
            {
                _context.Appointments.Remove(appointment);
                _context.SaveChanges();
            }
            return RedirectToAction("MyAppointments");
        }


        
        public IActionResult BarberAppointments()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appointments = _context.Appointments
                .Where(a => a.Barber.Email == userId)
                .Include(a => a.Client)
                .ToList();
            return View(appointments);
        }



        [Authorize(Roles = "Barber")]
        public IActionResult CancelBarberAppointment(int id) 
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                _context.SaveChanges();
            }
            return RedirectToAction("BarberAppointments");
        }



        [Authorize(Roles = "Admin")]
        public IActionResult AdminAppointments()
        {
            var appointments = _context.Appointments
                .Include(a => a.Client)
                .Include(a => a.Barber)
                .ToList();
            return View(appointments);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult AssignAppointment(int id)
        {
            var appointment = _context.Appointments.Find(id);
            ViewBag.Barbers = _context.Barbers.ToList();
            return View(appointment);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AssignAppointment(int id, int barberId)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment != null)
            {
                appointment.BarberId = barberId;
                _context.SaveChanges();
            }
            return RedirectToAction("AdminAppointments");
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
