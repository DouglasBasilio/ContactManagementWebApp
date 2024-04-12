using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alfasoft.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private bool CustomerExists(int id)
        {
            return _dbContext.Customers.Any(e => e.ID == id);
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var customers = _dbContext.Customers.Where(c => !c.IsDeleted).ToList();
            return View(customers);
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("/Home/Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Home/Create")]
        public async Task<IActionResult> Create([Bind("ID,Name,ContactNumber,EmailAddress")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Add(customer);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        [Route("/Home/Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _dbContext.Customers.FirstOrDefaultAsync(m => m.ID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [Route("/Home/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _dbContext.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Home/Edit/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,ContactNumber,EmailAddress")] Customer customer)
        {
            if (id != customer.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Update(customer);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.ID))
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
            return View(customer);
        }

        [Route("/Home/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _dbContext.Customers.FirstOrDefaultAsync(m => m.ID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("/Home/Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _dbContext.Customers.FindAsync(id);
            if (customer != null)
            {
                customer.IsDeleted = true;
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}