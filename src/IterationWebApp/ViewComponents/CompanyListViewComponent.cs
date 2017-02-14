using IterationWebApp.Models;
using IterationWebApp.ViewModels;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IterationWebApp.ViewComponents
{
    [ViewComponent(Name = "CompanyList")]
    public class CompanyListViewComponent : ViewComponent
    {
        private IIterationRepository _repository;

        
        public CompanyListViewComponent(IIterationRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            var companies = _repository.GetAllCompany();
            
            return View(companies);
            
        }
    }
}
