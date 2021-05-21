using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishLearning.Models
{
    public class WordCategoryVM
    {
        public WordCategoryVM()
        {
            Words = new HashSet<WordVM>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<WordVM> Words { get; set; }
    }
}
