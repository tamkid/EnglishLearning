using EnglishLearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishLearning.Services.Interfaces
{
    public interface IWordCategoryService
    {
        Task<List<WordCategorySelectionListVM>> GetCategorySelectionList();
    }
}
