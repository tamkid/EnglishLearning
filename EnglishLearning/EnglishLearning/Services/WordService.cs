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
            var lstResult = await _unitOfWork.Repository<Word>()
                                .Get(orderBy: e => e.OrderBy(o => o.EnglishWord),  includeProperties: "WordCategory")
                                .ProjectTo<WordVM>(_mapper.ConfigurationProvider)
                                .ToListAsync();
            return lstResult;
        }

        public async Task<WordEditVM> GetByIdForEdit(Guid id)
        {
            var entity = await _unitOfWork.Repository<Word>().GetById(id);
            if (entity != null)
            {
                var result = new WordEditVM()
                {
                    Id = entity.Id,
                    EnglishWord = entity.EnglishWord,
                    Mean = entity.Mean,
                    Type = entity.Type.Value,
                    Spelling = entity.Spelling,
                    WordCategoryId = entity.WordCategoryId
                };
                return result;
            }
            return null;
        }

        public async Task<int> Create(WordCreateVM model)
        {
            try
            {
                // Check duplicate word
                var existed = await _unitOfWork.Repository<Word>()
                    .Get(o => o.EnglishWord.Trim().ToLower() == model.EnglishWord.Trim().ToLower()).FirstOrDefaultAsync();
                if (existed != null)
                {
                    return -1; // duplicate
                }

                var entity = new Word()
                {
                    Id = Guid.NewGuid(),
                    EnglishWord = model.EnglishWord,
                    Mean = model.Mean,
                    Spelling = model.Spelling,
                    Type = model.Type,
                    WordCategoryId = model.WordCategoryId,
                    CreatedDate = DateTime.Now
                };
                await _unitOfWork.Repository<Word>().Insert(entity);
                return await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                return -2; // exception
            }
        }

        public async Task<int> Update(WordEditVM model)
        {
            try
            {
                var existed = await _unitOfWork.Repository<Word>().GetById(model.Id);
                if (existed == null) return -1; // Not existed

                // Check duplicate
                var duplicate = _unitOfWork.Repository<Word>()
                    .Get(o => o.Id != model.Id && o.EnglishWord.Trim().ToLower() == model.EnglishWord.Trim().ToLower())
                    .FirstOrDefault();
                if (duplicate != null) return -2; // duplicate

                // Bind 
                existed.EnglishWord = model.EnglishWord;
                existed.Mean = model.Mean;
                existed.Type = model.Type;
                existed.Spelling = model.Spelling;
                existed.WordCategoryId = model.WordCategoryId;
                return await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                return -3; // exception
            }
        }

        public async Task<int> Delete(Guid id)
        {
            try
            {
                var entity = await _unitOfWork.Repository<Word>().GetById(id);
                if (entity != null)
                {
                    _unitOfWork.Repository<Word>().Delete(entity);
                    return  await _unitOfWork.SaveChange();
                }
                return -1;
            }
            catch (Exception ex)
            {
                return -2;
            }
        }
    }
}
