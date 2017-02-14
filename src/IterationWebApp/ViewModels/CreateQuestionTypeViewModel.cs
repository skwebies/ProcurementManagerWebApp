using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IterationWebApp.ViewModels
{
    public class CreateQuestionTypeViewModel
    {
        [Required(ErrorMessage ="* Required!")]
        [Display(Name ="Question Type")]
        public string Question_Type { get; set; }
    }
}
