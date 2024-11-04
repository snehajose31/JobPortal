using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Models
{
    public class UserProfile
    {
        [Key]
        public int ProfileId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }  // Navigation property to User

        // Basic details
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        // Qualification details
        public string HighestQualification { get; set; }
        public int GraduationYear { get; set; }
        public string Skills { get; set; } // Comma-separated skills
    }
}
