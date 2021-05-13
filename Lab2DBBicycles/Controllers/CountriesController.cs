using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab2DBBicycles;

namespace Lab2DBBicycles.Controllers
{
    public class CountriesController : Controller
    {
        private readonly Lab2DbContext _context;

        public CountriesController(Lab2DbContext context)
        {
            _context = context;
            /*_context.Sales.RemoveRange(_context.Sales);
            _context.Bicycles.RemoveRange(_context.Bicycles);
            _context.Brands.RemoveRange(_context.Brands);
            _context.Countries.RemoveRange(_context.Countries);
            _context.SaveChanges();*/
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            return View(await _context.Countries.ToListAsync());
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id", "Name")] Country country)
        {
            if (ModelState.IsValid)
            {
                _context.Add(country);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Country country)
        {
            if (id != country.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.Id))
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
            return View(country);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            var brands = _context.Brands.Where(e => e.CountryId.Equals(id)).Select(e => e.Id);
            var bicycles = _context.Bicycles.Where(e => brands.Where(b => b.Equals(e.BrandId)).Any()).Select(e => e.Id);
            var sales = _context.Sales.Where(e => bicycles.Where(b => b.Equals(e.BicycleId)).Any()).Select(e => e.Id);
            ViewBag.brands = brands.Count();
            ViewBag.bicycles = bicycles.Count();
            ViewBag.sales = sales.Count();

            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var country = _context.Countries
                .Include(e => e.Brands)
                .Include($"{ nameof(Lab2DbContext.Brands)}.{nameof(Brand.Bicycles)}")
                .Include($"{ nameof(Lab2DbContext.Brands)}.{nameof(Brand.Bicycles)}.{nameof(Bicycle.Sales)}")
                .ToList().Find(e => e.Id == id);

            for (int i = 0; i < country.Brands.Count; ++i)
            {
                var brand = country.Brands.ElementAt(i);
                for (int j = 0; j < brand.Bicycles.Count; ++j)
                {
                    var bic = brand.Bicycles.ElementAt(j);
                    for (int k = 0; k < bic.Sales.Count; ++k)
                    {
                        var sale = bic.Sales.ElementAt(k);
                        bic.Sales.Remove(sale);
                    }
                    _context.SaveChanges();
                    brand.Bicycles.Remove(bic);
                }
                _context.SaveChanges();
                country.Brands.Remove(brand);
            }
            _context.SaveChanges();

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }
    }
}
