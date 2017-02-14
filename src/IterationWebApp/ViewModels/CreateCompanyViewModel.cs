using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IterationWebApp.ViewModels
{
    public class CreateCompanyViewModel
    {
        [Display(Name ="Company Name")]
        [Required(ErrorMessage ="Company name is required!")]
        public string CompanyName { get; set; }

        [Display(Name ="Corporate Number")]
        public string CorporateNumber { get; set; }

    }
}
