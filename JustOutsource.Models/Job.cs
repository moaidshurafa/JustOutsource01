//using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
//using System.ComponentModel.DataAnnotations;
//using JustOutsource.Models;

//namespace JustOutsource.Models
//{
//    public class Job
//    {
//        public int Id { get; set; }

//        [Required]
//        [StringLength(50)]
//        public string Title { get; set; }
//        [Required]
//        [StringLength(1000)]
//        public string Description { get; set; }
//        [Required]

//        public string RequiredSkills { get; set; }

//        public int CategoryId { get; set; }
//        [ValidateNever]
//        public Category Category { get; set; }
//    }
//}
