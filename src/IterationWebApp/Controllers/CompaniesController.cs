using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using IterationWebApp.Models;
using IterationWebApp.ViewModels;
using Microsoft.AspNet.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace IterationWebApp.Controllers
{
    [Authorize]
    [RequireHttps]
    public class CompaniesController : Controller
    {
        private IIterationRepository _repository;

        public CompaniesController(IIterationRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var companies = _repository.GetAllCompany();
            return View(companies);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateCompanyViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateCompanyViewModel vmCompany)
        {
            if (!ModelState.IsValid)
                return View(vmCompany);
       
            _repository.AddCompany(vmCompany);
            return RedirectToAction("Create", "Procurements");
        }
    }
}
