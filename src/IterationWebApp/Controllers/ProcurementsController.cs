using System;
using Microsoft.AspNet.Mvc;
using IterationWebApp.Models;
using IterationWebApp.ViewModels;
using Microsoft.AspNet.Authorization;
using System.Linq;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using System.Net;

namespace IterationWebApp.Controllers
{
    [Authorize]
    [RequireHttps]
    public class ProcurementsController : Controller
    {
        private IIterationRepository _repository;

        //Constructor
        public ProcurementsController(IIterationRepository repository)
        {
            _repository = repository;
        }

        #region Index Action Method for Procurement
        //Index Method for Procurement 
        public IActionResult Index(string keyword, string keyword2, string keyword3, DateTime? datetimepicker2, string SortOrder, string SortBy, string Page)
        {

            var procurements = _repository.GetAllProcurement();

            #region Sorting section
            switch (SortBy)
            {
                case "Company_Name":
                    switch (SortOrder)
                    {
                        case "Asc":
                            procurements = _repository.GetAllProcurementOrderByCompanyName();
                            break;
                        case "Desc":
                            procurements = _repository.GetAllProcurementOrderByDesc();
                            break;
                        default:
                            break;

                    }
                    break;
                case "Service_Procured":
                    switch (SortOrder)
                    {
                        case "Asc":
                            procurements = _repository.GetAllProcurementOrderByServiceProcured();
                            break;
                        case "Desc":
                            procurements = _repository.GetAllProcurementDescByServiceProcured();
                            break;
                        default:
                            break;

                    }
                    break;
                case "Response_Evaluation":
                    switch (SortOrder)
                    {
                        case "Asc":
                            procurements = _repository.GetAllProcurementOrderByResEval();
                            break;
                        case "Desc":
                            procurements = _repository.GetAllProcurementDescByResEval();
                            break;
                        default:
                            break;

                    }
                    break;
                case "Date_Of_Submission":
                    switch (SortOrder)
                    {
                        case "Asc":
                            procurements = _repository.GetAllProcurementOrderByResEval();
                            break;
                        case "Desc":
                            procurements = _repository.GetAllProcurementDescByResEval();
                            break;
                        default:
                            break;

                    }
                    break;

            }
            #endregion

            #region Search Functions
            //search by procurements
            if (!String.IsNullOrEmpty(keyword) && String.IsNullOrEmpty(keyword2) && datetimepicker2 == null && String.IsNullOrEmpty(keyword3))
            {
                procurements = _repository.GetSpecificProcurement(keyword);


            }
            //search by company
            if (!String.IsNullOrEmpty(keyword2) && String.IsNullOrEmpty(keyword) && datetimepicker2 == null && String.IsNullOrEmpty(keyword3))
            {
                procurements = _repository.GetSpecificProcurementByCompany(keyword2);
                //TempData["error"] = "Matching records not found!";
            }

            //search by response
            if (!String.IsNullOrEmpty(keyword3) && String.IsNullOrEmpty(keyword) && String.IsNullOrEmpty(keyword2) && datetimepicker2 == null)
            {
                procurements = _repository.GetSpecificProcurementByResponse(keyword3);

            }

            if (datetimepicker2 != null && String.IsNullOrEmpty(keyword3) && String.IsNullOrEmpty(keyword) && String.IsNullOrEmpty(keyword2))
            {
                procurements = _repository.GetSpecificProcurementByDate(datetimepicker2);
            }


            #endregion

            #region Pagination Section
            var pageSize = 4;
            var Total = _repository.GetTotalProcurements();


            ViewBag.SortOrder = SortOrder;
            ViewBag.SortBy = SortBy;

            ViewBag.TotalPages = Math.Ceiling(Total / pageSize);

            int page = int.Parse(Page == null ? "1" : Page);
            ViewBag.Page = page;

            var previousPage = page - 1;
            var nextPage = page + 1;

            ViewBag.PreviousPage = previousPage;
            ViewBag.HasPreviousPage = previousPage > 0;
            ViewBag.NextPage = nextPage;
            ViewBag.HasNextPage = nextPage < ViewBag.TotalPages;

            procurements = procurements.Skip((page - 1) * pageSize).Take(10);
            #endregion
            return View(procurements);
        }
        #endregion

        #region Create Procurement
        public IActionResult Create()
        {
            var viewModel = _repository.LoadCompanies(new CreateProcurementViewModel());
            return View(viewModel);
        }


        //Create Procurements
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateProcurementViewModel vmProcurement)
        {
            if (!ModelState.IsValid)
            {
                return View(vmProcurement);
            }

            _repository.AddProcurement(vmProcurement);
            TempData["success"] = vmProcurement.Service_Procured + " is added successfully!";
            return RedirectToAction("Index");
        }
        #endregion

        #region Detail Action method
        //Details
        public IActionResult Detail(long id)
        {

            var procurement = _repository.GetProcurementById(id);


            // return ViewComponent("ProcurementDetail", procurement.Procurement_Id);
            return View(procurement);
        }

        #endregion
        #region Delete Procurement
        //Delete 
        public IActionResult DeleteProcurement(long? id)
        {
            //_repository.DeleteProcurement(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(400);
            }
            Procurement pro = _repository.GetByID(id);
            if (pro == null)
            {
                return HttpNotFound();
            }

            return View(pro);
        }

        //Confirm Delete
        [HttpPost]
        [ActionName("DeleteProcurement")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmation(long id)
        {
            _repository.DeleteProcurement(id);
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Edit Procurement
        //Edit Action for Procurement
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(400);
            }

            EditProcurementViewModel vmPro = _repository.Edit(id);
            if (vmPro == null)
            {
                return HttpNotFound();
            }

            _repository.LoadCompaniesToEdit(vmPro);
            return View(vmPro);
        }


        //Edit post action for procurement
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Procurement Pro)
        {

            if (!ModelState.IsValid)
                return View(Pro);

            _repository.UpdateProcurement(Pro);
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
