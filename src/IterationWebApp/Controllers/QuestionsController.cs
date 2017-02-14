using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using IterationWebApp.Models;
using IterationWebApp.ViewModels;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Http;
using System.IO;
using Microsoft.AspNet.Hosting;
using Microsoft.Net.Http.Headers;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace IterationWebApp.Controllers
{
    [Authorize]
    [RequireHttps]
    public class QuestionsController : Controller
    {
        private IIterationRepository _repository;
        private IHostingEnvironment _environment;

        public QuestionsController(IIterationRepository repository, IHostingEnvironment environment)
        {
            _repository = repository;
            _environment = environment;
        }
        #region Searching Method
        [HttpGet]
        public IActionResult Index(string keyword1, string keyword2, string keyword3, string keyword4)
        {
            var questions = _repository.GetAllQuestionWithQuestionTypeAndProcurement();



            //search by question 
            if (!String.IsNullOrEmpty(keyword1) && String.IsNullOrEmpty(keyword2))
            {
                questions = _repository.GetSpecificQuestion(keyword1);

            }
            //search by procurements
            if (!String.IsNullOrEmpty(keyword2) && String.IsNullOrEmpty(keyword1))
            {
                questions = _repository.GetSpecificQuestionsByProcurement(keyword2);

            }

            //search by review score
            if (!String.IsNullOrEmpty(keyword3) && String.IsNullOrEmpty(keyword1) && String.IsNullOrEmpty(keyword2))
            {
                questions = _repository.GetSpecificQuestionByReviewScore(keyword3);

            }

            //search by winning response
            if (!String.IsNullOrEmpty(keyword4) && String.IsNullOrEmpty(keyword3) && String.IsNullOrEmpty(keyword1) && String.IsNullOrEmpty(keyword2))
            {
                questions = _repository.GetSpecificQuestionByWinningResponse(keyword4);

            }

            return View(questions);


        }
        #endregion

        #region Adding Questions
        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = _repository.LoadProcurementsAndQuestionTypes(new CreateQuestionViewModel());
            return View(viewModel);
        }


        //adding questions
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateQuestionViewModel vmQuestion, ICollection<IFormFile> Files)
        {
            if (!ModelState.IsValid)
                return View(vmQuestion);

            _repository.AddQuestions(vmQuestion, Files);
            return RedirectToAction("Index");
        }
        #endregion

        #region Open uploaded file
        //open uploaded files on the browser...
        public IActionResult OpenFile(IFormFile file)
        {
            
            try
            {
                
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                if(fileName == null)
                {
                    ViewData["errOr"] = "File not found";
                }

                var path = Path.Combine(_environment.WebRootPath, "Documents");
                var fs = System.IO.File.OpenRead(path + fileName);

                return File(fs, "application/pdf", fileName);


            }
            catch
            {
                throw;
            }

        }
        #endregion

        #region Edit Question
        //Edit Action Methods
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(400);
            }

            EditQuestionViewModel vmQues = _repository.EditQuestion(id);
            if (vmQues == null)
            {
                return HttpNotFound();
            }

            _repository.LoadProAndQuesToEdit(vmQues);
            return View(vmQues);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Question ques, ICollection<IFormFile> Files)
        {
            if (!ModelState.IsValid)
                return View(ques);


            //string oldFileUrl = _repository.GetQuestionById(ques.Question_Id).Documents;

            //File uploading
            var uploads = Path.Combine(_environment.WebRootPath, "Documents");
            var stringUrls = new List<string>();
            foreach (var file in Files)
            {
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');


                    file.SaveAs(Path.Combine(uploads, fileName));
                    var relPath = "~/Documents/" + fileName;

                    stringUrls.Add(relPath);
                }
                

            }
            EditQuestionViewModel vm = new EditQuestionViewModel();
            vm.FileUrl = string.Join(",", stringUrls);
            ques.Documents = vm.FileUrl;
            _repository.UpdateQuestion(ques);

            return RedirectToAction("Index");
        }
        #endregion

        #region Delete Question
        //Delete 
        public IActionResult DeleteQuestion(long? id)
        {
            //_repository.DeleteProcurement(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(400);
            }
            Question ques = _repository.GetQuestionById(id);
            if (ques == null)
            {
                return HttpNotFound();
            }

            return View(ques);
        }

        //Confirm Delete
        [HttpPost]
        [ActionName("DeleteQuestion")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmation(long? id)
        {
            _repository.DeleteQuestion(id);
            return RedirectToAction("Index", "Questions");
        }
        #endregion
    }
}
