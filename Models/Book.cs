using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.App3.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        public string Title { get; set; }

        [Required]
        [StringLength(32)]
        public string Author { get; set; }

        [Required]
        [StringLength(32)]
        public string Publisher { get; set; }

        [Required]
        public int PageCount { get; set; }

        [Required]
        [StringLength(32)]
        public string Genre { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int CostPrice { get; set; }

        [Required]
        public int SellingPrice { get; set; }

        [Required]
        public bool IsContinuation { get; set; }

        [Required]
        public int RatingBook { get; set; }

        [Required]
        public int RatingAuthor { get; set; }

        [Required]
        public DateTime DateAdd { get; set; } //формат : 2021-05-01 


    }
}
