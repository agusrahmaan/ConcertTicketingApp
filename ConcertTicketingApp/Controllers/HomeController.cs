using ConcertTicketingApp.Data;
using ConcertTicketingApp.Models;
using ConcertTicketingApp.Models.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Tls;
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
            List<DataConcert> datas = _context.dataConcerts.Where(x => x.Status == "Published").ToList();
            return View(datas);
        }

        public IActionResult Detail(int id)
        {
            var concert = _context.dataConcerts.FirstOrDefault(x => x.Id == id);
            return View(concert);
        }

        public IActionResult Order(int id)
        {
            var getConscertId = _context.dataConcerts.Where(dc => dc.Id == id).FirstOrDefault();
            return View(getConscertId);
        }

        [HttpPost]
        public IActionResult Order([FromForm] OrderForm orderForm) 
        {
            var orders = new Order()
            {
                DataConcert = _context.dataConcerts.Where(x => x.Id == orderForm.DataConcertId).FirstOrDefault(),
                Name = orderForm.Name,
                Email = orderForm.Email,
                NoTelepon = orderForm.NoTelepon
            };
            _context.orders.Add(orders);
            _context.SaveChanges();

            var getKuotaConcert = _context.dataConcerts.Where(dc => dc.Id == orderForm.DataConcertId).FirstOrDefault();
            if(getKuotaConcert.KuotaTiket <= 0)
            {
                return BadRequest("Ticket is sold out!!!");
            }
            getKuotaConcert.KuotaTiket -= 1;

            _context.dataConcerts.Update(getKuotaConcert);
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