using IterationWebApp.Models;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace IterationWebApp.ViewComponents
{
    public class ProcurementListViewComponent : ViewComponent
    {
        private IIterationRepository _repository;

        public ProcurementListViewComponent(IIterationRepository repository)
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
