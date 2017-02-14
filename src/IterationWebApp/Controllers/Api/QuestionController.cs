using IterationWebApp.Models;
using IterationWebApp.ViewModels;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IterationWebApp.Controllers.Api
{
    [Route("api/questions")]
    public class QuestionController : Controller
    {
        private IIterationRepository _repository;

        public QuestionController(IIterationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("")]
        public JsonResult Get()
        {
            var results = _repository.GetAllQuestion();
            return Json(results);
        }

        [HttpPost("")]
        public JsonResult Post([FromBody]CreateQuestionViewModel question)
        {
            if (ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.Created;
                return Json(true);
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Failed");
        }
    }
}
