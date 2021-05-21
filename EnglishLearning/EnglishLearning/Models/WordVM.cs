using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishLearning.Models
{
    public class WordVM
    {
        public Guid Id { get; set; }
        public string EnglishWord { get; set; }
        public string Mean { get; set; }
        public string Spelling { get; set; }
        public int? Type { get; set; }
        public Guid? WordCategoryId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual WordCategoryVM WordCategory { get; set; }

        // Custom
        public string TypeName {
            get {
                return ((Enums.WordType)Type.Value).ToString();
            }
        }
    }
}
