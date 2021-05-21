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
    public class WordService : BaseService, IWordService
    {
        public WordService(IUnitOfWork unitOfWork, IMapper mapper): base(unitOfWork, mapper){}

        public async Task<List<WordVM>> GetAll()
        {
            try
            {
                var lstResult = await _unitOfWork.Repository<Word>()
                                .Get(includeProperties: "WordCategory")
                                .ProjectTo<WordVM>(_mapper.ConfigurationProvider)
                                .ToListAsync();
                return lstResult;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
