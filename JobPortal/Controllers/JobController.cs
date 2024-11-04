using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobPortal.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using JobPortal.Data;

public class JobController : Controller
{
    private readonly ApplicationDbContext _context;

    public JobController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Job
    public async Task<IActionResult> Index()
    {
        var jobs = await _context.JobDtls.Include(j => j.JobCategory).ToListAsync();
        return View(jobs);
    }

    public async Task<IActionResult> Create()
    {
        try
        {
            var jobCategories = await _context.JobCategories.ToListAsync();
            ViewBag.JobCategories = new SelectList(jobCategories, "Id", "Name");
            return View(new JobDtl()); // Ensure you're passing a new instance of JobDtl
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return View("Error");
        }
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("JobTitle,Description,Location,PostingDate,JobCategoryId")] JobDtl jobDtl)
    {
        if (ModelState.IsValid)
        {
            _context.JobDtls.Add(jobDtl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // If we get here, something went wrong; retrieve the job categories again
        var jobCategories = await _context.JobCategories.ToListAsync();
        ViewBag.JobCategories = new SelectList(jobCategories, "Id", "Name", jobDtl.JobCategoryId);
        return View(jobDtl); // Pass the jobDtl back to the view
    }



    public ActionResult Home()
    {
        var jobs = _context.JobDtls.Include("JobCategory").ToList();
        return View("Home", jobs); // Updated to render Home.cshtml
    }

    public IActionResult UserDashboard()
    {
        var jobList = _context.JobDetails.Where(j => j.IsActive).ToList();
        return View(jobList);
    }

    // Action to display application form
    public IActionResult Apply(int id)
    {
        var job = _context.JobDetails.FirstOrDefault(j => j.JobId == id);
        if (job == null)
        {
            return NotFound();
        }

        var model = new JobApplication { JobId = id };
        return View(model);
    }

    // POST: Job/Apply
    [HttpPost]
    public IActionResult Apply(JobApplication application)
    {
        if (ModelState.IsValid)
        {
            // Ensure the JobId is valid
            var jobExists = _context.JobDetails.Any(j => j.JobId == application.JobId);
            if (!jobExists)
            {
                ModelState.AddModelError("JobId", "The specified job does not exist.");
                return View(application);
            }

            application.ApplicationDate = DateTime.Now; // Set the application date
            _context.JobApplications.Add(application);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(application);
    }


    // Application confirmation view
    public IActionResult ApplicationConfirmation()
    {
        return View();
    }

}
