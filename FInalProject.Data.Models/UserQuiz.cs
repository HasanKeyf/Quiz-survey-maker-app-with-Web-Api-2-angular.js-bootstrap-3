using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FInalProject.Data.Models
{
   public class UserQuiz
    {

        public int UserQuizId { get; set; }

        public int QuziId { get; set; }
        [ForeignKey("QuziId")]
        public virtual QuizSurvey QZ { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        
   
       public virtual ApplicationUser User { get; set; }


    }
}
