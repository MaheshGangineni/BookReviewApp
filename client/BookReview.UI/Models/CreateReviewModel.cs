using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookReview.UI.Models
{
    public class CreateReviewModel
    {
        [Required]
        public string BookName { get; set; }
        [Required]
        public string Review { get; set; }
    }
}
