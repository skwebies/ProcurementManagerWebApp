using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using IterationWebApp.ViewModels;
using IterationWebApp.Models;
using Microsoft.AspNet.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace IterationWebApp.Controllers
{
    [Authorize]
    [RequireHttps]
    public class QuestionTypesController : Controller
    {
        private IIterationRepository _repository;

        public QuestionTypesController(IIterationRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var types = _repository.GetAllQuestionTypes();
            return View(types);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateQuestionTypeViewModel());
        }

        [HttpPost][ValidateAntiForgeryToken]
        public  IActionResult Create(CreateQuestionTypeViewModel vmQuestionType)
        {
            if (!ModelState.IsValid)
            {
                return View(vmQuestionType);
            }
            _repository.AddQuestionType(vmQuestionType);
             //TempData["Msg"] = vmQuestionType.Question_Type + " Added Successfully";
            return RedirectToAction("Create","Questions");
        }

       
        public IActionResult Delete(long? id)
        {
            _repository.DeleteQuestionType(id);
            return View();
        }
    }
}
