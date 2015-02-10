using FInalProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FInalProject.Models.UserViewModels
{
    public class QuizSurveyViewModel
    {
        public int QuizSurveyId { get; set; }
        public string QuizSurveyTitle { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdated { get; set; }
        public QZTypes Type { get; set; }
        public int AttendeCount{ get; set; }
        public List<QuestionViewModel> Questions { get; set; }
    }
}