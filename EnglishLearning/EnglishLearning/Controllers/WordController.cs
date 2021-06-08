using EnglishLearning.Models;
using EnglishLearning.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishLearning.Controllers
{
    public class WordController : Controller
    {
        private readonly IWordService _wordService;
        private readonly IWordCategoryService _wordCateService;

        public WordController(IWordService wordService, IWordCategoryService wordCateService)
        {
            _wordService = wordService;
            _wordCateService = wordCateService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            WordCreateVM model = new WordCreateVM();
            model.ListWordCategory = await _wordCateService.GetCategorySelectionList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(WordCreateVM model)
        {
            if (ModelState.IsValid)
            {
                int result = await _wordService.Create(model);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    if (result == 0 || result == -2)
                    {
                        ModelState.AddModelError("", "Unable to add the word");
                    }
                    else if (result == -1)
                    {
                        ModelState.AddModelError("", $"Word {model.EnglishWord} is existed");
                    }
                }
            }

            model.ListWordCategory = await _wordCateService.GetCategorySelectionList();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            WordEditVM model = await _wordService.GetByIdForEdit(id);
            if (model != null)
            {
                model.ListWordCategory = await _wordCateService.GetCategorySelectionList();
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(WordEditVM model)
        {
            if (ModelState.IsValid)
            {
                int result = await _wordService.Update(model);
                if (result >= 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    if (result == -1)
                    {
                        ModelState.AddModelError("", "Unable to find the word");
                    }
                    else if (result == -2)
                    {
                        ModelState.AddModelError("", $"Word {model.EnglishWord} is existed");
                    }
                    else if (result == -3)
                    {
                        ModelState.AddModelError("", $"Unable to update the word");
                    }
                }
            }

            model.ListWordCategory = await _wordCateService.GetCategorySelectionList();
            return View(model);
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> GetWordTable()
        {
            List<WordVM> model = await _wordService.GetAll();
            return PartialView("_WordTable", model);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteWord(string id)
        {
            //Guid idG = Guid.Parse(id);
            //int result = await _wordService.Delete(idG);
            //return Ok(result);
            return Ok();
        }

        #endregion
    }
}
