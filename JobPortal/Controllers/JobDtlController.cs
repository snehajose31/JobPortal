using Microsoft.AspNetCore.Mvc;
using JobPortal.Models;
using JobPortal.Data;
using Microsoft.EntityFrameworkCore; // Import your DbContext

namespace JobPortal.Controllers
{
    public class JobDltController : Controller
    {
        private readonly ApplicationDbContext _context; // Inject DbContext

        public JobDltController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JobDlt/Create
        public IActionResult Create()
        {
            // Populate a list of JobCategories for the dropdown
            ViewBag.JobCategories = _context.JobCategories.ToList();
            return View();
        }

        // POST: JobDlt/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobId,JobTitle,Description,Location,PostingDate,JobCategoryId")] JobDtl jobDtl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobDtl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.JobCategories = _context.JobCategories.ToList();
            return View(jobDtl);
        }

        // GET: JobDlt/Index
        public async Task<IActionResult> Index()
        {
            return View(await _context.JobDtls.ToListAsync());
        }

        // GET: JobDlt/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobDtl = await _context.JobDtls
                .Include(j => j.JobCategory) // Include related JobCategory
                .FirstOrDefaultAsync(m => m.JobId == id);

            if (jobDtl == null)
            {
                return NotFound();
            }

            return View(jobDtl);
        }

        // GET: JobDlt/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobDtl = await _context.JobDtls.FindAsync(id);
            if (jobDtl == null)
            {
                return NotFound();
            }
            ViewBag.JobCategories = _context.JobCategories.ToList();
            return View(jobDtl);
        }

        // POST: JobDlt/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobId,JobTitle,Description,Location,PostingDate,JobCategoryId")] JobDtl jobDtl)
        {
            if (id != jobDtl.JobId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobDtl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobDtlExists(jobDtl.JobId))
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
            ViewBag.JobCategories = _context.JobCategories.ToList();
            return View(jobDtl);
        }

        // GET: JobDlt/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobDtl = await _context.JobDtls
                .Include(j => j.JobCategory)
                .FirstOrDefaultAsync(m => m.JobId == id);
            if (jobDtl == null)
            {
                return NotFound();
            }

            return View(jobDtl);
        }

        // POST: JobDlt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobDtl = await _context.JobDtls.FindAsync(id);
            _context.JobDtls.Remove(jobDtl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobDtlExists(int id)
        {
            return _context.JobDtls.Any(e => e.JobId == id);
        }
    }
}