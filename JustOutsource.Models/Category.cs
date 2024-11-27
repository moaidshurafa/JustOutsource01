using System.ComponentModel.DataAnnotations;

namespace JustOutsource.Models
{
    public class Category
    {

        
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string CategoryName { get; set; }
        [Required]
        [StringLength(250)]
        public string CategoryDescription { get; set; }

    }
}
