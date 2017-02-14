using IterationWebApp.Models;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IterationWebApp.ViewComponents
{
    public class QuestionListViewComponent : ViewComponent
    {
        private IIterationRepository _repository;

        public QuestionListViewComponent(IIterationRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            var questions = _repository.GetAllQuestionWithQuestionTypeAndProcurement();
            return View(questions);
        } 
    }
}
