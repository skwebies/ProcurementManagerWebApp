using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IterationWebApp.Models
{
    public class QuestionType
    {
        [Key]
        public long Question_Type_Id { get; set; }

        public string Question_Type { get; set; }


        public ICollection<Question> Questions { get; set; }


    }
}
