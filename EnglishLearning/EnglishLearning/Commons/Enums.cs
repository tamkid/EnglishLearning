using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishLearning
{
    public class Enums
    {
        public enum WordType
        { 
            [Display(Name = "Noun")]
            N = 1,
            [Display(Name = "Verb")]
            V = 2,
            [Display(Name = "Adjective")]
            Adj = 3,
            [Display(Name = "Adverb")]
            Adv = 4
        }
    }
}
