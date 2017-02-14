using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IterationWebApp.Models
{
    public class Question
    {
        [Key]
        public long Question_Id { get; set; }

        public string Question_Number { get; set; }

        public string question { get; set; }

        public string Iteration_Response { get; set; }

        public string Review_Score_Iteration { get; set; }

        public string Winning_Response { get; set; }

        public string Review_Score_Winning_Company { get; set; }

        public string Note { get; set; }

        public string Documents { get; set; }



        public long ProcurementID { get; set; }

        public virtual Procurement Procurement { get; set; }


        public long QuestionTypeID { get; set; }

        public virtual QuestionType QuestionType { get; set; }

        //public ICollection<Procurement> Procurements { get; set; }





    }
}
