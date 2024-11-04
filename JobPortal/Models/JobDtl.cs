using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Models
{
    public class JobDtl
    {
        [Key]
        public int JobId { get; set; } // Primary Key

        [Required]
        public string JobTitle { get; set; } // Title of the job

        [Required]
        public string Description { get; set; } // Job description

        [Required]
        public string Location { get; set; } // Job location

        [Required]
        public DateTime PostingDate { get; set; } // Date of posting

        // Foreign key for JobCategory
        public int JobCategoryId { get; set; }

        // Navigation property for JobCategory
        [ForeignKey("JobCategoryId")]
        public virtual JobCategory JobCategory { get; set; }
    }
}
