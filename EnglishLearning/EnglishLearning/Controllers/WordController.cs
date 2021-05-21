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

        public WordController(IWordService wordService)
        {
            _wordService = wordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> GetWordTable()
        {
            List<WordVM> model = await _wordService.GetAll();
            return PartialView("_WordTable", model);
        }

        #endregion
    }
}
