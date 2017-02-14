using IterationWebApp.Models;
using IterationWebApp.ViewModels;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IterationWebApp.ViewComponents
{
    public class SearchProcurementViewComponent : ViewComponent
    {
        private IIterationRepository _repository;

        public SearchProcurementViewComponent(IIterationRepository repository)
        {
            _repository = repository; 
        }

        public IViewComponentResult Invoke()
        {
            var procurements = _repository.DisplayAllProcurements();

            

            return View(procurements);
        }
    }
}
