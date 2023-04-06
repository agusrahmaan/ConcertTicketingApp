using ConcertTicketingApp.Data;
using ConcertTicketingApp.Models;
using ConcertTicketingApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ConcertTicketingApp.Controllers
{
    public class ConcertController : Controller
    {
        private readonly MySqlContext _context;
        public ConcertController(MySqlContext c)
        {
            _context = c;
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
        public IActionResult Add([FromForm] DataConcert dataConcert)
        {
            
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
        public IActionResult Edit([FromForm] DataConcert dataConcert)
        {
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
    }
}
