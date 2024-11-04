using Microsoft.AspNetCore.Mvc;
using JobPortal.Data;
using JobPortal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Controllers
{
    public class JobDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JobDetails/Index
        public async Task<IActionResult> Index()
        {
            // Fetch job details with associated category info and send to view
            var jobDetails = await _context.JobDetails.Include(j => j.JobCategory).ToListAsync();
            return View(jobDetails);
        }

        // GET: JobDetails/Create
        public async Task<IActionResult> Create()
        {
            // Fetch categories for dropdown list and check if any are available
            var categories = await _context.JobCategories.ToListAsync();
            if (!categories.Any())
            {
                ViewBag.ErrorMessage = "No job categories found. Please add categories before creating a job.";
                return View();
            }

            ViewBag.JobCategoryId = new SelectList(categories, "JobCategoryId", "CategoryName");
            return View();
        }

        // POST: JobDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobDetails jobDetails)
        {
            
                // Add the job details to the context and save
                await _context.JobDetails.AddAsync(jobDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            

            // If validation fails, reload categories and show form again
            ViewBag.JobCategoryId = new SelectList(await _context.JobCategories.ToListAsync(), "JobCategoryId", "CategoryName", jobDetails.JobCategoryId);
            return View(jobDetails);
        }

        // GET: JobDetails/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var jobDetails = await _context.JobDetails.FindAsync(id);
            if (jobDetails != null)
            {
                // Remove the job details if it exists and save changes
                _context.JobDetails.Remove(jobDetails);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Home()
        {
            // Fetch job details with associated category info and send to view
            var jobDetails = await _context.JobDetails.Include(j => j.JobCategory).ToListAsync();
            return View(jobDetails);
        }

    }
}
