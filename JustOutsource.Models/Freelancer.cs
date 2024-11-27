using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JustOutsource.Models
{
    public class Freelancer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        [Required]

        public string Skills { get; set; }
        [Required]

        [StringLength(1000)]
        public string ProfileDescription { get; set; }

        public int CategoryId { get; set; }
        [ValidateNever]
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        [Required]
        [Display(Name = "From ")]
        [Range(0, 10000)]
        public double ListPrice { get; set; }

        //[Required]
        //[Display(Name = "From ")]
        //[Range(0, 10000)]
        //public double Price { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }


    }
}
