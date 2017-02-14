using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IterationWebApp.Models
{
    public class Company
    {
        [Key]
        public long Company_Id { get; set; }

        public string Company_Name { get; set; }

        public string Corporate_Number { get; set; }


        public ICollection<Procurement> Procurements { get; set; }


    }
}
