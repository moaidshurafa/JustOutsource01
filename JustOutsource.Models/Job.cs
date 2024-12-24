using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JustOutsource.Models
{
    public class Job
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
        [Required]
        [StringLength(80)]
        public string RequiredSkills { get; set; }

        public int CategoryId { get; set; }
        [ValidateNever]
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [Required]
        [Display(Name = "From ")]
        [Range(0, 10000)]
        public double Budget { get; set; }
        [Required]
        [EmailAddress]
        public string EmailToContact { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [ValidateNever]
        public string AdditionalFile { get; set; }
        [Url]
        [StringLength(2083)]
        public string? CompanyOrJobWebSite { get; set; }
        [Required]
        public JobType Type { get; set; }
        public string? UserId { get; set; }

    }
    public enum JobType
    {
        FullTime,
        PartTime,
        Freelance
    }
}
