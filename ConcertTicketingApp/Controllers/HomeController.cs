using ConcertTicketingApp.Data;
using ConcertTicketingApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ConcertTicketingApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MySqlContext _context;
        public HomeController(
            ILogger<HomeController> logger,
            MySqlContext c)
        {
            _logger = logger;
            _context = c;
        }

        public IActionResult Index()
        {
            List<DataConcert> datas = _context.dataConcerts.ToList();
            return View(datas);
        }

        public IActionResult Detail(int id)
        {
            var concert = _context.dataConcerts.FirstOrDefault(x => x.Id == id);
            return View(concert);
        }

        public IActionResult Order()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Order([FromForm]Order order) 
        { 
           
            _context.orders.Add(order);
            _context.SaveChanges();

            return RedirectToAction("Index");
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