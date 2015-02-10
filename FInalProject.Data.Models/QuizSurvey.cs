using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FInalProject.Data.Models
{
   public class QuizSurvey
    {

       public QuizSurvey()
       {
           this.CreatedAt = DateTime.Now;
           this.Reference = Guid.NewGuid();
           this.IsActive = true;
       }

       [Key]
        public int QuizSurveyId { get; set; }

       [Required]
       public string QuestionSurveyTitle { get; set; }

       [Column(TypeName = "datetime2")]
       public DateTime CreatedAt { get; set; }
       [Column(TypeName = "datetime2")]
       public DateTime? UpdatedAt { get; set; }
        
       [Required]
       public QZTypes Type { get; set; }

       [MaxLength(2000)]
       public string Description { get; set; }
       public Guid Reference { get; set; }

       public bool IsActive { get; set; }

       public string UserId { get; set; }
       [ForeignKey("UserId")]
       public virtual ApplicationUser User { get; set; }

       public virtual List<Question> Questions { get; set; }


    }

  public enum QZTypes
   {
       Quiz,
       Survey
   }
}
