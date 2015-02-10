using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FInalProject.Models
{
    public class AnswerViewModel
    {
        public int AnswerId { get; set; }
        public bool IsCorrect { get; set; }
        public string AnswerTitle { get; set; }
        public int QuestionId { get; set; }
    }
}