using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustOutsource.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int FreelancerId { get; set; }
        [ForeignKey("FreelancerId")]
        [ValidateNever]
        public Freelancer Freelancer { get; set; }
        [Range(1, 10, ErrorMessage = "Please Enter a number of days between 1 - 10")]
        public int CountDays { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        [NotMapped]
        public double Price { get; set; }
    }
}
