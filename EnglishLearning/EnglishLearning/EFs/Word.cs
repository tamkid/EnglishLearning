using System;
using System.Collections.Generic;

#nullable disable

namespace EnglishLearning.EFs
{
    public partial class Word
    {
        public Guid Id { get; set; }
        public string EnglishWord { get; set; }
        public string Mean { get; set; }
        public string Spelling { get; set; }
        public int? Type { get; set; }
        public Guid? WordCategoryId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual WordCategory WordCategory { get; set; }
    }
}
