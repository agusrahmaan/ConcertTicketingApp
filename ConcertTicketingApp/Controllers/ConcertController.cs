using ConcertTicketingApp.Data;
using ConcertTicketingApp.Models;
using ConcertTicketingApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConcertTicketingApp.Controllers
{
    [Authorize]
    public class ConcertController : Controller
    {
        private readonly MySqlContext _context;
        private readonly IWebHostEnvironment _env;
        public ConcertController(MySqlContext c, IWebHostEnvironment env)
        {
            _context = c;
            _env = env;
        }
        public IActionResult Index()
        {
            var datas = _context.dataConcerts.ToList();
            return View(datas);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add([FromForm] DataConcert dataConcert, IFormFile Photo)
        {
            

            if (!ModelState.IsValid)
            {
                return View();
            }

            var filename = "photo_" + dataConcert.Musisi + Path.GetExtension(Photo.FileName);
            var filepath = Path.Combine(_env.WebRootPath, "upload", filename);

            using (var stream = System.IO.File.Create(filepath))
            {
                Photo.CopyTo(stream);
            }

            dataConcert.Photo = filename;

            _context.dataConcerts.Add(dataConcert);
            _context.SaveChanges();
        
            return RedirectToAction("Index");
        } 

        public IActionResult Edit(int id)
        {
            var concert = _context.dataConcerts.FirstOrDefault(x => x.Id == id);
            return View(concert);
        }

        [HttpPost]
        public IActionResult Edit([FromForm] DataConcert dataConcert, IFormFile Photo)
        {
            var filename = "photo_" + dataConcert.Musisi + Path.GetExtension(Photo.FileName);
            var filepath = Path.Combine(_env.WebRootPath, "upload", filename);

            using (var stream = System.IO.File.Create(filepath))
            {
                Photo.CopyTo(stream);
            }

            dataConcert.Photo = filename;

            _context.dataConcerts.Update(dataConcert);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var concert = _context.dataConcerts.FirstOrDefault(x => x.Id == id);

            _context.dataConcerts.Remove(concert);
            _context.SaveChanges();
            

            return RedirectToAction("Index");
        }

        public IActionResult DetailOrder()
        {
            var order = _context.orders.ToList();
            return View(order);
        }

        public IActionResult Deleted(int id)
        {
            var del = _context.orders.FirstOrDefault(x => x.Id == id);
            var addBack = _context.orders.Include(x => x.DataConcert).FirstOrDefault(x => x.Id == id);
            addBack.DataConcert.KuotaTiket += 1;


            _context.orders.Update(addBack);
            _context.orders.Remove(del);
            _context.SaveChanges();



            return RedirectToAction("DetailOrder");
        }
    }
}
