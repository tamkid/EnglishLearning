using System;
using System.Collections.Generic;

#nullable disable

namespace EnglishLearning.EFs
{
    public partial class WordCategory
    {
        public WordCategory()
        {
            Words = new HashSet<Word>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<Word> Words { get; set; }
    }
}
