using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FInalProject.Data.Models
{
   public class Answer
    {
       public Answer()
       {
           this.IsCorrectAnswer = false;
       }

       [Key]
        public  int AnswerId { get; set; }
        public string Answertitle { get; set; }
        public bool IsCorrectAnswer { get; set; }

        public virtual int QuestinId { get; set; }
        [ForeignKey("QuestinId")]
        public virtual Question Question { get; set; }
    }


}
