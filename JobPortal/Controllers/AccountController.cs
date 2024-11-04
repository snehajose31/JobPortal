using JobPortal.Data;
using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace JobPortal.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                // Store user info in TempData for session-like behavior
                TempData["Username"] = user.Username;
                TempData["Role"] = user.Role;

                if (user.Role == "Admin")
                    return RedirectToAction("AdminDashboard");
                else
                    return RedirectToAction("UserDashboard");
            }
            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        // GET: Account/Logout
        public IActionResult Logout()
        {
            TempData.Clear();
            return RedirectToAction("Login");
        }

        // GET: Account/AdminDashboard
        public IActionResult AdminDashboard()
        {
            if (TempData["Role"]?.ToString() == "Admin")
                return View();
            return RedirectToAction("Login");
        }

        // GET: Account/UserDashboard
        public IActionResult UserDashboard()
        {
            if (TempData["Role"]?.ToString() == "User")
                return View();
            return RedirectToAction("Login");
        }

        // GET: Account/Profile
        public IActionResult CreateProfile()
        {
            return View(new UserProfile());
        }

        // Action to handle the form submission for creating the profile
        [HttpPost]
        public IActionResult CreateProfile(UserProfile userProfile)
        {
            if (ModelState.IsValid)
            {
                var username = TempData["Username"] as string;
                var user = _context.Users.FirstOrDefault(u => u.Username == username);

                if (user != null)
                {
                    userProfile.UserId = user.UserId; // Set UserId in the profile
                    _context.UserProfiles.Add(userProfile);
                    _context.SaveChanges();
                    return RedirectToAction("Profile"); // Redirect to the profile view after saving
                }
            }

            // Log the ModelState errors
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage); // Use a proper logging mechanism in production
            }

            return View(userProfile); // If ModelState is invalid, return to the form
        }



        // Action to view the user's profile
        public IActionResult Profile()
        {
            var username = TempData["Username"] as string;

            if (!string.IsNullOrEmpty(username))
            {
                var userProfile = _context.UserProfiles
                                           .Include(up => up.User)  // Include User to access Username, Email, and Phone
                                           .FirstOrDefault(up => up.User.Username == username);

                if (userProfile != null)
                {
                    return View(userProfile); // Pass UserProfile with User details to the view
                }
            }

            return RedirectToAction("Login"); // Redirect to Login if no profile is found
        }


        //////////////////////
        public async Task<IActionResult> ManageUsers()
        {
            var users = await _context.Users.ToListAsync(); // Fetch all users
            return View(users); // Pass the user list to the view
        }

        // POST: Admin/ToggleUserActivation
        [HttpPost]
        public async Task<IActionResult> ToggleUserActivation(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                user.IsActive = !user.IsActive; // Toggle the IsActive status
                await _context.SaveChangesAsync(); // Save changes to the database
            }
            return RedirectToAction("ManageUsers"); // Redirect back to user management
        }
    }
}

