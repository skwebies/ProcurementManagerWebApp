using IterationWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IterationWebApp.ViewModels
{
    public class DisplayProcurementViewModel
    {
        public long Procurement_Id { get; set; }

        public string Service_Procured { get; set; }

        public DateTime? Date_Of_Submission { get; set; }

        public string Response_Evaluation { get; set; }

        public string Note { get; set; }
        
        public string CompanyName { get; set; }

        public string CorporateNumber { get; set; }

        public ICollection<Question> Questions { get; set; }

        public ICollection<QuestionType> Qtypes { get; set; }


        
    }
}
