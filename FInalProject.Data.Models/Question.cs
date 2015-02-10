using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FInalProject.Data.Models
{
    public class Question
    {
        public Question()
        {
            this.CreatedAt = DateTime.Now;
            if (this.UpdatedAt == null)
            {
                this.UpdatedAt = DateTime.Now;
            }
        }

        [Key]
        public int QuestionId { get; set; }
        [Required]
        public string QuestionTitle { get; set; }
        [MaxLength(2000)]
        public string QuestionDescription { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? UpdatedAt { get; set; }

      
        public virtual int QuizOrSurveyId { get; set; }
        [ForeignKey("QuizOrSurveyId")]
        public virtual QuizSurvey QZ { get; set; }
    }
}
