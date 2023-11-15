using CinemaManager_GDG2.Models.Cinema;
using CinemaManager_GDG2.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaManager_GDG2.Controllers
{
    public class ProducersController : Controller
    {
        CinemaDbGdg2Context _context;
        public ProducersController(CinemaDbGdg2Context context)
        {
            _context = context;
        }
        // GET: ProducersController
        public ActionResult Index()
        {
            return View(_context.Producers.ToList());
        }

        public ActionResult ProdsAndTheirMovies()
        {
            _context.Movies.ToList();
            return View(_context.Producers.ToList());
        }
        public IActionResult ProdsAndTheirMovies_UsingModel()
        {
            var prods = _context.Producers.ToList();
            var movies = _context.Movies.ToList();
            var query = from p in prods
                        join m in movies
                        on p.Id equals m.ProducerId
                        select new ProdMovie
                        {
                            mTitle = m.Title,
                            mGenre = m.Genre,
                            pName = p.Name,
                            pNat = p.Nationality
                        };
            //ViewBag.q = query.ToList();
            return View(query.ToList());
        }

        public IActionResult MyMovies(int id)
        {
            var movies = _context.Movies.ToList();
            List<Movie> PM = _context.Movies.Where(m => m.ProducerId == id).ToList();
            var PM2 = from m in movies where m.ProducerId == id select m;
            return View(PM);
        }

        // GET: ProducersController/Details/5
        public ActionResult Details(int id)
        {
            return View(_context.Producers.Find(id));
        }

        // GET: ProducersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProducersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Producer p)
        {
            try
            {
                _context.Producers.Add(p);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProducersController/Edit/5
        public ActionResult Edit(int id)
        {
            Producer p = _context.Producers.Find(id);
            return View(p);
        }

        // POST: ProducersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Producer p)
        {
            try
            {
                _context.Producers.Update(p);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProducersController/Delete/5
        public ActionResult Delete(int id)
        {
            Producer p = _context.Producers.Find(id);
            return View(p);
        }

        // POST: ProducersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Producer p)
        {
            try
            {
                _context.Producers.Remove(p);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
