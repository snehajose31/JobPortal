using JobPortal.Data;
using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Controllers
{
    public class JobCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JobCategory
        public async Task<IActionResult> Index()
        {
            var categories = await _context.JobCategories.ToListAsync();
            return View(categories);
        }

        // GET: JobCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobCategory jobCategory)
        {
            if (ModelState.IsValid)
            {
                _context.JobCategories.Add(jobCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobCategory);
        }

        // GET: JobCategory/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var jobCategory = await _context.JobCategories.FindAsync(id);
            if (jobCategory == null)
            {
                return NotFound();
            }
            return View(jobCategory);
        }

        // POST: JobCategory/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JobCategory jobCategory)
        {
            if (id != jobCategory.JobCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobCategoryExists(jobCategory.JobCategoryId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(jobCategory);
        }

        // GET: JobCategory/Delete/{id}
        // GET: Admin/DeleteCategory/{id}
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var jobCategory = await _context.JobCategories.FindAsync(id);
            if (jobCategory == null)
            {
                return NotFound(); // Return 404 if the category is not found
            }

            return RedirectToAction("Index");
        }

        // POST: Admin/DeleteCategory/{id}
        [HttpPost, ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategoryConfirmed(int id)
        {
            var jobCategory = await _context.JobCategories.FindAsync(id);
            if (jobCategory != null)
            {
                _context.JobCategories.Remove(jobCategory);
                await _context.SaveChangesAsync(); // Save changes to the database
            }
            return RedirectToAction("ManageJobCategories"); // Redirect to the management page after deletion
        }
        private bool JobCategoryExists(int id)
        {
            return _context.JobCategories.Any(e => e.JobCategoryId == id);
        }
    }
}
