using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookReview.UI.Models
{
    public class BookReviewModel
    {
        public int BookId { get; set; }
        [Required]
        public string BookName { get; set; }
        [Required]
        public string Review { get; set; }
    }
}
