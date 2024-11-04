using System.ComponentModel.DataAnnotations;

namespace JobPortal.Models
{
    public class JobCategory
    {
        public int JobCategoryId { get; set; } // Primary Key

        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; } // Name of the job category
    }
}
