using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FInalProject.Models
{
    public class QuizTakerViewModel
    {
        public int QuizTakerId { get; set; }

        public string FullName { get; set; }

        public int CorrectCount { get; set; }

        public int WrongCount { get; set; }
    }
}