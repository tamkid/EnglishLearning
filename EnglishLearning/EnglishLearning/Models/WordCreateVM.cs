using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishLearning.Models
{
    public class WordCreateVM
    {
        [Required]
        [Display(Name = "Word")]
        public string EnglishWord { get; set; }
        [Required]
        [Display(Name = "Meaning")]
        public string Mean { get; set; }
        public string Spelling { get; set; }
        public int Type { get; set; }
        [Display(Name = "Word Category")]
        public Guid? WordCategoryId { get; set; }

        // Selection List
        public List<WordCategorySelectionListVM> ListWordCategory { get; set; }
    }
}
