using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FInalProject.Data.Models
{
   public class QuizTaker
    {
       [Key] 
       public int QuizTakerId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int QuizSurveyId { get; set; }
       

    }
}
