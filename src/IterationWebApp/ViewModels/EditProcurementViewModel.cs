using IterationWebApp.Models;
using Microsoft.AspNet.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IterationWebApp.ViewModels
{
    public class EditProcurementViewModel
    {

        public long Procurement_Id { get; set; }

        [Display(Name = "Service Procured")]
        [Required(ErrorMessage = "You must enter service procured")]
        public string Service_Procured { get; set; }

        //public DateTime? Date_Of_Submission { get; set; }
        [Display(Name = "Response Evaluation")]
        [Required(ErrorMessage = "Response evaluation is required!")]
        public string Response_Evaluation { get; set; }

        [Display(Name = "Company")]
        [Required(ErrorMessage = "Please select a company")]
        public long CompanyID { get; set; }

        //public long  CompanyID { get; set; }


        public List<SelectListItem> Companies { get; set; }

        public virtual Company Company { get; set; }

        public string CompanyName { get; set; }

        public string Note { get; set; }


        public DateTime? ModifiedDate { get; set; }

        public string PublishedDate { get; set; }


    }
}
