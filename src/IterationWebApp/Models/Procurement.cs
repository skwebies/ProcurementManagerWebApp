using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IterationWebApp.Models
{
    public class Procurement 
    {
        [Key]
        public long Procurement_Id { get; set; }

        public string Service_Procured { get; set; }

        
        public  DateTime? Date_Of_Submission { get; set; }

        public string PublishedDate { get; set; }

        public string Response_Evaluation { get; set; }

        public string Note { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public long CompanyID { get; set; }

        public virtual Company Company { get; set; }

        //public virtual Question question{ get; set; }

        //public ICollection<Company> Companies { get; set; }
        public ICollection<Question> Questions { get; set; }





    }
}
