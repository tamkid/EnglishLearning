using AutoMapper;
using EnglishLearning.EFs;
using EnglishLearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishLearning.ExConfigs
{
    public class AutoMapperConfig: Profile
    {
        public AutoMapperConfig() 
        {
            CreateMap<Word, WordVM>();
            CreateMap<WordCategory, WordCategoryVM>();
        }
    }
}
