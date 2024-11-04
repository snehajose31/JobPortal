using System;
using System.ComponentModel.DataAnnotations;

namespace JobPortal.Models
{
    public class JobDetails
    {
        [Key]
        public int JobId { get; set; }

        [Required]
        public string JobTitle { get; set; } = string.Empty;  // Default initialization

        public string Description { get; set; } = string.Empty;  // Default initialization

        [Required]
        public decimal Salary { get; set; }

        public string CompanyName { get; set; } = string.Empty;  // Default initialization

        [Required]
        public string JobLocation { get; set; } = string.Empty;  // Default initialization

        public string Requirements { get; set; } = string.Empty;  // Default initialization

        public string PreferredQualifications { get; set; } = string.Empty;  // Default initialization

        public DateTime PostedDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;

        // Foreign key for JobCategory
        [Required]
        public int JobCategoryId { get; set; }
        public JobCategory JobCategory{ get; set; }  // Navigation property
    }
}
