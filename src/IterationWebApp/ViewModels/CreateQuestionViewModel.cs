using IterationWebApp.Models;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IterationWebApp.ViewModels
{
    public class CreateQuestionViewModel
    {
        [Display(Name ="Question Number")]
        public string Question_Number { get; set; }

        [Display(Name ="Question")]
        [Required(ErrorMessage ="Please write the question")]
        public string question { get; set; }

        [Display(Name ="Iteration Response")]
        [Required(ErrorMessage ="Iteration Response is required!")]
        public string Iteration_Response { get; set; }

        [Display(Name ="Review Score Iteration")]
        public string Review_Score_Iteration { get; set; }

        [Display(Name ="Winning Response")]
        public string Winning_Response { get; set; }

        [Display(Name ="Review Score Winning Company")]
        public string Review_Score_Winning_Company { get; set; }

        [Display(Name ="Note")]
        public string Note { get; set; }

        public List<SelectListItem> QuestionTypes { get; set; }

        [Display(Name ="Question Type")]
        public long SelectedQuestionTypeID { get; set; }


        public List<SelectListItem> Procurements { get; set; }

        [Display(Name ="Service Procured")]
        public long SelectedProcurementID { get; set; }


        [DataType(DataType.Upload)]
        [Display(Name = "File")]
        public virtual ICollection<IFormFile> Files { get; set; }

        public string FileUrl { get; set; }


        public virtual Procurement Procurement { get; set; }

        public virtual QuestionType QuestionType { get; set; }

    }
}
