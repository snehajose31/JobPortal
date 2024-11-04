using System.ComponentModel.DataAnnotations;

    namespace JobPortal.Models
    {
        public class User
        {
            [Key]
            public int UserId { get; set; }

            [Required]
            public string Username { get; set; }

            [Required]
            public string Password { get; set; }

            [Required]
            public string Phone { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public string Role { get; set; } // "Admin" or "User"

            public bool IsActive { get; set; } // To track user status
        }
    }

