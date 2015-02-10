using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FInalProject.Data.Models
{
    public class QuestionAnswer
    {
        [Key]
        public int QuestionAnserId { get; set; }

        public int QuizId { get; set; }
        [ForeignKey("QuizId")]
        public virtual QuizSurvey Quiz { get; set; }

        public int AnswerId { get; set; }
        [ForeignKey("AnswerId")]
        public virtual Answer Answer { get; set; }

        public int QuizTakerId { get; set; }
        [ForeignKey("QuizTakerId")]
        public virtual QuizTaker QuizTaker { get; set; }
    }
}
