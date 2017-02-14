using IterationWebApp.Models;
using IterationWebApp.ViewModels;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IterationWebApp.ViewComponents
{
    public class ProcurementDetailViewComponent : ViewComponent
    {
        private IIterationRepository _repository;

        public ProcurementDetailViewComponent(IIterationRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke(long id)
        {
            
            var procurement = _repository.GetProcurementById(id);
            
                return View(procurement);
            
            
        }
    }
}
