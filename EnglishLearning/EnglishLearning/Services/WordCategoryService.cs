using AutoMapper;
using AutoMapper.QueryableExtensions;
using EnglishLearning.EFs;
using EnglishLearning.Models;
using EnglishLearning.Repositories.Interfaces;
using EnglishLearning.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishLearning.Services
{
    public class WordCategoryService : BaseService, IWordCategoryService
    {
        public WordCategoryService(IUnitOfWork unitOfWork, IMapper mapper): base(unitOfWork, mapper){}

        public async Task<List<WordCategorySelectionListVM>> GetCategorySelectionList()
        {
            try
            {
                var lstResult = await _unitOfWork.Repository<WordCategory>().Get()
                                    .Select(o => new WordCategorySelectionListVM()
                                    {
                                        Id = o.Id,
                                        Name = o.Name
                                    }).ToListAsync();
                var empty = new WordCategorySelectionListVM()
                {
                    Id = null,
                    Name = "--- Select ---"
                };
                lstResult.Insert(0, empty);
                return lstResult;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
