using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using IterationWebApp.Models;
using Microsoft.AspNet.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace IterationWebApp.Controllers
{
    [Authorize]
    [RequireHttps]
    public class HomeController : Controller
    {
        private IIterationRepository _repository;

        public HomeController(IIterationRepository repository)
        {
            _repository = repository;
        }

        //home page action
        [HttpGet]
        public IActionResult Index(string SortBy, string SortOrder,string Page)
        {
            var procurements = _repository.GetAllProcurement();

            #region Sorting section
            switch (SortBy)
            {
                case "Company_Name":
                    switch (SortOrder)
                    {
                        case "Asc":
                            procurements = _repository.GetAllProcurement().OrderBy(p=>p.Company.Company_Name).ToList();
                            break;
                        case "Desc":
                            procurements = _repository.GetAllProcurement().OrderByDescending(p=>p.Company.Company_Name).ToList();
                            break;
                        default:
                            break;

                    }
                    break;
                case "Service_Procured":
                    switch (SortOrder)
                    {
                        case "Asc":
                            procurements = _repository.GetAllProcurement().OrderBy(p=>p.Service_Procured).ToList();
                            break;
                        case "Desc":
                            procurements = _repository.GetAllProcurement().OrderByDescending(p=>p.Service_Procured).ToList();
                            break;
                        default:
                            break;

                    }
                    break;
                case "Response_Evaluation":
                    switch (SortOrder)
                    {
                        case "Asc":
                            procurements = _repository.GetAllProcurement().OrderBy(p=>p.Response_Evaluation).ToList();
                            break;
                        case "Desc":
                            procurements = _repository.GetAllProcurement().OrderByDescending(p=>p.Response_Evaluation).ToList();
                            break;
                        default:
                            break;

                    }
                    break;
                case "Date_Of_Submission":
                    switch (SortOrder)
                    {
                        case "Asc":
                            procurements = _repository.GetAllProcurement().OrderBy(p=>p.Date_Of_Submission).ToList();
                            break;
                        case "Desc":
                            procurements = _repository.GetAllProcurement().OrderByDescending(p=>p.Date_Of_Submission).ToList();
                            break;
                        default:
                            break;

                    }
                    break;
                case "Corporate_Number":
                    switch (SortOrder)
                    {
                        case "Asc":
                            procurements = _repository.GetAllProcurement().OrderBy(p => p.Company.Corporate_Number).ToList();
                            break;
                        case "Desc":
                            procurements = _repository.GetAllProcurement().OrderByDescending(p => p.Company.Corporate_Number).ToList();
                            break;
                        default:
                            break;

                    }
                    break;

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

            procurements = procurements.Skip((page - 1) * pageSize).Take(8).ToList();
            #endregion

            return View(procurements);
        }

        public IActionResult Error()
        {
            return View();
        }

    }
}
