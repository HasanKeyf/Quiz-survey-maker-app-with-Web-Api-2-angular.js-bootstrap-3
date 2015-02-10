using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FInalProject.Models.UserViewModels
{
    public class QuizSurveyShareViewModel
    {
        public int QuizSurveyId { get; set; }
        public string QuizSurveyTitle { get; set; }

        public string EmailToShare { get; set; }
        public string Link { get; set; }
        
    }
}