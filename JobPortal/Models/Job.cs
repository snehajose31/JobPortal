using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Models
{
    public class Job
    {
        public int JobId { get; set; } // Primary Key

        [Required]
        [StringLength(100)]
        public string JobTitle { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Salary { get; set; }

        [Required]
        [StringLength(100)]
        public string CompanyName { get; set; }

        public string Requirement { get; set; }

        public string Qualification { get; set; }

        [ForeignKey("JobCategory")]
        public int JobCategoryId { get; set; } // Foreign Key

        public JobCategory JobCategory { get; set; } // Navigation Property
    }
}
