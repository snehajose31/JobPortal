namespace JobPortal.Models
{
    public class JobApplication
    {
        public int ApplicationId { get; set; }
        public int JobId { get; set; }
        public string ApplicantName { get; set; }
        public string ApplicantEmail { get; set; }
        public DateTime ApplicationDate { get; set; }

        // Navigation property
        public JobDetails JobDetails { get; set; }
    }
}
